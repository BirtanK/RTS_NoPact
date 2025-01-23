using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour {
    public Camera Camera;
    public List<Figure> Selected = new();
    public Transform SelectionIndicator;

    #region Orders
        public enum CommandType {
            Move,
            Attack
        }
        public interface IIssuedOrder { public CommandType Command { get; set; } }
        public struct MoveOrder : IIssuedOrder { 
            public CommandType Command { get; set; }
            public Vector3 TargetPoint;
        }
        public struct AttackOrder : IIssuedOrder {
            public CommandType Command { get; set; }
            public Figure Target;
        }
    #endregion

    //

    private Figure lastTouchedFigure;
    private Ray ray;
    private RaycastHit info;
    private void Update() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            ray = Camera.ScreenPointToRay(Input.GetTouch(0).position);
        } else if (Input.GetMouseButtonDown(0)) {
            ray = Camera.ScreenPointToRay(Input.mousePosition);
        } else return;

        if (Physics.Linecast(ray.origin, (160 * ray.direction) + ray.origin, out info, 192)) {
            lastTouchedFigure = info.collider.GetComponent<Figure>();

            if (Selected.Count == 0) {
                // Select
                if (lastTouchedFigure != null && lastTouchedFigure.IsCommandableByPlayer) {
                    Selected.Add(lastTouchedFigure);

                    SelectionIndicator.SetParent(lastTouchedFigure._transform, false);
                    SelectionIndicator.gameObject.SetActive(true);
                }
            } else {
                if (lastTouchedFigure == null) {
                    // Move
                    foreach (Figure figure in Selected) {
                        figure.Order = new MoveOrder { 
                            Command = CommandType.Move,
                            TargetPoint = info.point
                        };
                    }
                } else {
                    if (Selected.Contains(lastTouchedFigure)) {
                        Selected.Clear();
                        SelectionIndicator.gameObject.SetActive(false);
                        return;
                    }

                    // Attack / Deselect & Select
                    if (lastTouchedFigure.IsCommandableByPlayer) {  // Deselect & Select
                        Selected.Clear();
                        Selected.Add(lastTouchedFigure);

                        SelectionIndicator.SetParent(lastTouchedFigure._transform, false);
                    } else if (!lastTouchedFigure.IsFriendlyFigure) { // Attack
                        foreach (Figure figure in Selected) {
                            figure.Order = new AttackOrder { 
                                Command = CommandType.Attack,
                                Target = lastTouchedFigure
                            };
                        }
                    } else {    // Deselect
                        Selected.Clear();
                        SelectionIndicator.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(ray.origin, info.point);
        Gizmos.DrawCube(info.point, 0.5f * Vector3.one);
    }
}
