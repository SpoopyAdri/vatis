using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Vatsim.Vatis.Config;

[XmlType("Facility")]
[Serializable]
public class LegacyFacility
{
    [XmlAttribute] public string ID { get; set; }
    [XmlAttribute] public string Name { get; set; }
    [XmlAttribute] public DateTime UpdatedOn { get; set; }
    public int Frequency { get; set; }
    public string AtisFrequency { get; set; }
    public string VoiceServer { get; set; }
    public string InformationDisplaySystemEndpoint { get; set; }
    public bool VoiceRecordEnabled { get; set; }
    public LegacyMagneticVariation MagneticVariation { get; set; }
    public LegacyObservationTime MetarObservation { get; set; }
    public List<LegacyContraction> Contractions { get; set; }
    public List<LegacyProfile> Profiles { get; set; }
    public List<LegacyNotam> Notams { get; set; }
    public List<LegacyAirportCondition> AirportConditions { get; set; }
}

[XmlType("Contraction")]
[Serializable]
public class LegacyContraction
{
    [XmlAttribute] public string Key { get; set; }
    [XmlAttribute] public string Value { get; set; }
}

[XmlType("Profile")]
[Serializable]
public class LegacyProfile
{
    [XmlAttribute] public string Name { get; set; }
    public string AirportConditions { get; set; }
    public string Notams { get; set; }
    public string AtisTemplate { get; set; }
    public string Approaches { get; set; }
}

[XmlType("Notam")]
[Serializable]
public class LegacyNotam
{
    [XmlAttribute] public string Message { get; set; }
    [XmlAttribute] public bool IsSelected { get; set; }
}

[XmlType("AirportCondition")]
[Serializable]
public class LegacyAirportCondition
{
    [XmlAttribute] public string Message { get; set; }
    [XmlAttribute] public bool IsSelected { get; set; }
}

[Serializable]
public class LegacyMagneticVariation
{
    [XmlAttribute] public int MagneticVariationValue { get; set; }
    [XmlAttribute] public LegacyMagneticVariationOption Option { get; set; }

    [Serializable]
    public enum LegacyMagneticVariationOption
    {
        Subtract,
        Add,
        None
    }
}

[Serializable]
public class LegacyObservationTime
{
    [XmlAttribute] public int ObservationTimeValue { get; set; }
    [XmlAttribute] public bool Enable { get; set; }
}