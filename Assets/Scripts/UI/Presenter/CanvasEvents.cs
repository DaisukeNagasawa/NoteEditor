﻿using NoteEditor.UI.Model;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace NoteEditor.UI.Presenter
{
    public class CanvasEvents : MonoBehaviour
    {
        public readonly Subject<Vector3> NotesRegionOnMouseUpObservable = new Subject<Vector3>();
        public readonly Subject<Vector3> NotesRegionOnMouseExitObservable = new Subject<Vector3>();
        public readonly Subject<Vector3> NotesRegionOnMouseDownObservable = new Subject<Vector3>();
        public readonly Subject<Vector3> NotesRegionOnMouseEnterObservable = new Subject<Vector3>();
        public readonly Subject<Vector3> VerticalLineOnMouseDownObservable = new Subject<Vector3>();
        public readonly Subject<Vector3> WaveformRegionOnMouseDownObservable = new Subject<Vector3>();
        public readonly Subject<Vector3> WaveformRegionOnMouseExitObservable = new Subject<Vector3>();
        public readonly Subject<Vector3> WaveformRegionOnMouseEnterObservable = new Subject<Vector3>();
        public readonly Subject<float> MouseScrollWheelObservable = new Subject<float>();

        void Awake()
        {
            this.UpdateAsObservable()
                .Select(_ => Input.GetAxis("Mouse ScrollWheel"))
                .Where(delta => delta != 0)
                .Subscribe(MouseScrollWheelObservable.OnNext);

            var model = NotesEditorModel.Instance;
            NotesRegionOnMouseExitObservable.Select(_ => false)
                .Merge(NotesRegionOnMouseEnterObservable.Select(_ => true))
                .Subscribe(isMouseOver => model.IsMouseOverNotesRegion.Value = isMouseOver);

            WaveformRegionOnMouseExitObservable.Select(_ => false)
                .Merge(WaveformRegionOnMouseEnterObservable.Select(_ => true))
                .Subscribe(isMouseOver => model.IsMouseOverWaveformRegion.Value = isMouseOver);
        }

        public void NotesRegionOnMouseUp() { NotesRegionOnMouseUpObservable.OnNext(Input.mousePosition); }
        public void NotesRegionOnMouseExit() { NotesRegionOnMouseExitObservable.OnNext(Input.mousePosition); }
        public void NotesRegionOnMouseDown() { NotesRegionOnMouseDownObservable.OnNext(Input.mousePosition); }
        public void NotesRegionOnMouseEnter() { NotesRegionOnMouseEnterObservable.OnNext(Input.mousePosition); }
        public void VerticalLineOnMouseDown() { VerticalLineOnMouseDownObservable.OnNext(Input.mousePosition); }
        public void WaveformRegionOnMouseDown() { WaveformRegionOnMouseDownObservable.OnNext(Input.mousePosition); }
        public void WaveformRegionOnMouseExit() { WaveformRegionOnMouseExitObservable.OnNext(Input.mousePosition); }
        public void WaveformRegionOnMouseEnter() { WaveformRegionOnMouseEnterObservable.OnNext(Input.mousePosition); }
    }
}