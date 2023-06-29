using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalResult : MonoBehaviour {

    [SerializeField] private float panelOffset;
    [SerializeField] private float textOffset;
    [SerializeField] private float pastOffset;
    [SerializeField] private float panelSpeed;
    [SerializeField] private TextMeshProUGUI highScoreValue;
    [SerializeField] private Transform[] panels;
    [SerializeField] private List<Stat> statList;

    private int scalingUnit = 0;
    private Vector2[] panelPositions;
    private Stat prevStat;

    private enum State {
        Idle,
        HighscoreMovePast,
        HighscoreMoveNormal,
        HighscoreShow,
        StatsMovePast,
        StatsMoveNormal,
        StatsTextShow,
        StatsValueShow
    } private State state = State.Idle;

    private void Start() {
        panelPositions = new Vector2[2];
        for (int i = 0; i < panels.Length; i++) {
            panelPositions[i] = panels[i].position;
            panels[i].position = new Vector2(panels[i].position.x - panelOffset, panels[i].position.y);
        }
        foreach (Stat stat in statList) {
            var textTransform = stat.text.transform;
            stat.textPosition = textTransform.localPosition;
            textTransform.localPosition = new Vector2(textTransform.transform.localPosition.x - textOffset, textTransform.localPosition.y);
            stat.text.alpha = 0;
            stat.value.transform.localScale = Vector3.zero;
        }

        highScoreValue.transform.localScale = Vector3.zero;
    }

    private void Update() {
        switch (state) {
            case State.HighscoreMovePast:
                if ((Vector2) panels[0].position != new Vector2(panelPositions[0].x + pastOffset, panelPositions[0].y)) {
                    panels[0].position = Vector2.MoveTowards(panels[0].position, 
                                                             new Vector2(panelPositions[0].x + pastOffset, panelPositions[0].y), panelSpeed * Time.deltaTime * 2f);
                } else state = State.HighscoreMoveNormal;
                break;
            case State.HighscoreMoveNormal:
                if ((Vector2) panels[0].position != panelPositions[0]) {
                    panels[0].position = Vector2.MoveTowards(panels[0].position, panelPositions[0], panelSpeed * Time.deltaTime);
                } else state = State.HighscoreShow;
                break;
            case State.HighscoreShow:
                if (highScoreValue.transform.localScale != Vector3.one * 1.2f && scalingUnit <= 0) {
                    highScoreValue.transform.localScale = Vector3.MoveTowards(highScoreValue.transform.localScale, Vector3.one * 1.2f, Time.deltaTime * 6f);
                } else if (highScoreValue.transform.localScale != Vector3.one) {
                    scalingUnit = 1;
                    highScoreValue.transform.localScale = Vector3.MoveTowards(highScoreValue.transform.localScale, Vector3.one, Time.deltaTime * 3f);
                } else state = State.StatsMovePast;
                break;
            case State.StatsMovePast:
                if ((Vector2) panels[1].position != new Vector2(panelPositions[1].x + pastOffset, panelPositions[1].y)) {
                    panels[1].position = Vector2.MoveTowards(panels[1].position,
                                                             new Vector2(panelPositions[1].x + pastOffset, panelPositions[1].y), panelSpeed * Time.deltaTime * 2f);
                } else state = State.StatsMoveNormal;
                break;
            case State.StatsMoveNormal:
                if ((Vector2) panels[1].position != panelPositions[1]) {
                    panels[1].position = Vector2.MoveTowards(panels[1].position, panelPositions[1], panelSpeed * Time.deltaTime);
                } else state = State.StatsTextShow;
                break;
            case State.StatsTextShow:
                var stat = statList[scalingUnit - 1];
                if (stat.text.transform.localPosition != stat.textPosition && stat.text.alpha < 1) {
                    stat.text.transform.localPosition = Vector3.MoveTowards(stat.text.transform.localPosition, stat.textPosition, panelSpeed * Time.deltaTime * 0.5f);
                    stat.text.alpha = Mathf.MoveTowards(stat.text.alpha, 1, Time.deltaTime * 5f);
                } else if (scalingUnit < statList.Count) {
                    scalingUnit++;
                } else {
                    scalingUnit = 0;
                    state = State.StatsValueShow;
                } break;
            case State.StatsValueShow:
                var statVal = statList[scalingUnit];
                if (statVal.value.transform.localScale != Vector3.one * 1.2f && scalingUnit != statList.Count - 1) {
                    statVal.value.transform.localScale = Vector3.MoveTowards(statVal.value.transform.localScale, Vector3.one * 1.2f, Time.deltaTime * 8f);
                    if (prevStat != null) prevStat.value.transform.localScale = Vector3.MoveTowards(prevStat.value.transform.localScale, Vector3.one, Time.deltaTime * 4f);
                } else if (scalingUnit < statList.Count - 1) {
                    prevStat = statVal;
                    scalingUnit++;
                } else if (scalingUnit == statList.Count - 1 && statVal.value.transform.localScale != Vector3.one) {
                    if (prevStat != null) prevStat.value.transform.localScale = Vector3.MoveTowards(prevStat.value.transform.localScale, Vector3.one, Time.deltaTime * 4f);
                    statVal.value.transform.localScale = Vector3.MoveTowards(statVal.value.transform.localScale, Vector3.one, Time.deltaTime * 4f);
                } else state = State.Idle;
                break;
        }
    }

    public void BeginDisplay(ScoreData scoreData) {
        highScoreValue.text = scoreData.score.ToString();
        statList[0].value.text = scoreData.gameTime.x.ToString() + ":" + scoreData.gameTime.y.ToString();
        statList[1].value.text = scoreData.wavesPassed.ToString();
        statList[2].value.text = scoreData.goodSales.ToString();
        statList[3].value.text = scoreData.wrongSales.ToString();
        statList[4].value.text = scoreData.missedSales.ToString();
        statList[5].value.text = scoreData.foodsShot.ToString();
        state = State.HighscoreMovePast;
    }
}

[System.Serializable]
public class Stat {
    public string name;
    public TextMeshProUGUI text;
    public TextMeshProUGUI value;
    [HideInInspector] public Vector3 textPosition;
}