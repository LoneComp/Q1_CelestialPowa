using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(S_Planet))]
public class S_PlanetEditor : Editor
{
    private S_Planet planet;
    
    Editor shapeEditor;
    Editor colorEditor;
    
    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
          base.OnInspectorGUI();
          if (check.changed)
          {
              planet.GeneratePlanet();
          }
        }
        
        DrawSettingsEditor(planet.shapeSettings, planet.OnShapeSettingsChanged, ref planet.shapeFoldoutSetting, ref shapeEditor);
        DrawSettingsEditor(planet.colourSettings, planet.OnColourSettingsChanged, ref planet.colorFoldoutSetting, ref colorEditor);
    }

    void DrawSettingsEditor(Object _settings, System.Action _onSettingsUpdated, ref bool _foldout, ref Editor _editor)
    {
        if (_settings != null)
        {
            _foldout = EditorGUILayout.InspectorTitlebar(_foldout, _settings);
        
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                if (_foldout)
                {
                    CreateCachedEditor(_settings, null, ref _editor);
                    _editor.OnInspectorGUI();

                    if (check.changed)
                    {
                        if (_onSettingsUpdated != null)
                            _onSettingsUpdated();
                    }
                }
            }
        }
    }
    private void OnEnable()
    {
        planet = (S_Planet)target;
    }
}
