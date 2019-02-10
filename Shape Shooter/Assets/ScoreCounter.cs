using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.MessageSystem;
using Wokarol.SerializationSystem;

namespace Wokarol
{
    public class ScoreCounter : MonoBehaviour
    {
        private static string HighscoreID = "Highscore";

        [SerializeField] SaveData saveData = null;
        [SerializeField] FloatVariableReference currentScore;

        private void Awake() {
            currentScore.Value = 0;
            Messenger.Default.RegisterSubscriberTo<EnemyDestroyedEvent>(OnEnemyDestroyed);

            saveData.BeforeSave += SaveScore;
        }

        private void OnDestroy() {
            Messenger.Default.UnRegisterAllSubscribersForObjects(this);
        }

        private void SaveScore() {
            if (saveData.GetEntry<int>(HighscoreID, 0) > currentScore.Value) return;
            saveData.SendEntry(HighscoreID, (int)currentScore.Value);
        }

        void OnEnemyDestroyed(EnemyDestroyedEvent e) {
            //Debug.Log($"Killed {e.DestroyedEnemy.name} for <b>{e.PointsCount}</b> points");
            currentScore.Value += e.PointsCount;
        }
    } 
}
