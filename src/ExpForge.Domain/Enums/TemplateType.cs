namespace ExpForge.Domain.Enums;

public static class Template
{
    public enum TemplateType
    {
        Widget,
        Component,
        Net_GeoProcess
    }

    public static string ConvertTypeToFolderName(TemplateType templateType)
    {
        return templateType switch
        {
            TemplateType.Component => "components",
            TemplateType.Widget => "custom-widgets",
            TemplateType.Net_GeoProcess => "net-geoprocess",
            _ => throw new ArgumentOutOfRangeException(nameof(templateType), templateType, null),
        };
        ;
    }

}