using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class GUIOption
{
    public const int MaxControlHeight = 600,
                     MaxControlWidth = 500,
                     MinControlHeight = 10,
                     MinControlWidth = 10,

                     MaxNodeHeight = 1000,
                     MaxNodeWidth = 1000,
                     MinNodeHeight = 25,
                     MinNodeWidth = 100,

                     xPadding = -6,
                     yPadding = 1,
                     CloseButtonSize = 15;
    //TODO better naming
    public static readonly GUILayoutOption[] TextAreaOptions = new[] {
                                                                      GUILayout.ExpandHeight(true),GUILayout.MinHeight(MinControlHeight), GUILayout.MaxHeight(MaxControlHeight),
                                                                      GUILayout.ExpandWidth( false), GUILayout.MinWidth(MaxControlWidth),   GUILayout.MaxWidth(MaxControlWidth)
                                                                     },

                                              GUIOptions = new[] { GUILayout.ExpandHeight(false), GUILayout.ExpandWidth(true), GUILayout.MinWidth(MaxControlWidth) },

                                              SingleButtonOptions = new[] { GUILayout.ExpandHeight(false), GUILayout.ExpandWidth(false) };
}