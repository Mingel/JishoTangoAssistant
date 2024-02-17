using Avalonia.Controls;
using System;

namespace JishoTangoAssistant.UI.Elements;

public class MeaningCheckBox : CheckBox
{
    protected override Type StyleKeyOverride => typeof(CheckBox);

    public int MeaningsRow { get; set; }

    public int MeaningsColumn { get; set; }

    public int MeaningsFlattenedIndex { get; set; }
}
