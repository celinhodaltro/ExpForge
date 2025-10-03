using ExpForge.Domain.Enums;

namespace ExpForge.Domain.Extensions;

public static class TemplateExtension
{
    public static string ConvertTypeToFolderName(this TemplateType templateType)
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