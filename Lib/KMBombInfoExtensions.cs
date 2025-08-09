using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Souvenir;

public enum Battery
{
    Unknown = 0,
    Empty = 0,
    D = 1,
    AA = 2,
    AAx3 = 3,
    AAx4 = 4
}

public enum Port
{
    DVI,
    Parallel,
    PS2,
    RJ45,
    Serial,
    StereoRCA,
    ComponentVideo,
    CompositeVideo,
    USB,
    HDMI,
    VGA,
    AC,
    PCMCIA
}

public enum Indicator
{
    SND,
    CLR,
    CAR,
    IND,
    FRQ,
    SIG,
    NSA,
    MSA,
    TRN,
    BOB,
    FRK,
    NLL
}

public enum IndicatorColor
{
    Black,
    White,
    Blue,
    Gray,
    Green,
    Magenta,
    Orange,
    Purple,
    Red,
    Yellow
}

/// <summary>
/// Some helper extensions methods for the KMBombInfo class.
/// </summary>
public static class KMBombInfoExtensions
{
    #region JSON Types

    public static string WidgetQueryTwofactor = "twofactor";
    public static string WidgetTwofactorKey = "twofactor_key";

    private class IndicatorJSON
    {
        public string label = null;
        public string on = null;

        public bool IsOn() => bool.TryParse(on, out var isOn) && isOn;
    }

    private class ColorIndicatorJSON
    {
        public string label = null;
        public string color = null;
    }

    private class TwoFactorJSON
    {
        public int twofactor_key = 0;
    }

    private class BatteryJSON
    {
        public int numbatteries = 0;
    }

    private class PortsJSON
    {
        public string[] presentPorts = null;
    }

    private class SerialNumberJSON
    {
        public string serial = null;
    }

    #endregion

    #region Helpers

    private static IEnumerable<T> GetJSONEntries<T>(KMBombInfo bombInfo, string queryKey, string queryInfo)
        where T : new() => bombInfo.QueryWidgets(queryKey, queryInfo).Select(JsonConvert.DeserializeObject<T>);

    private static IEnumerable<IndicatorJSON> GetIndicatorEntries(KMBombInfo bombInfo) =>
        GetJSONEntries<IndicatorJSON>(bombInfo, KMBombInfo.QUERYKEY_GET_INDICATOR, null).Where(x => x != null);

    private static IEnumerable<ColorIndicatorJSON> GetColorIndicatorEntries(KMBombInfo bombInfo) =>
        GetJSONEntries<ColorIndicatorJSON>(bombInfo, KMBombInfo.QUERYKEY_GET_INDICATOR + "Color", null).Where(x => x != null);

    private static IEnumerable<BatteryJSON> GetBatteryEntries(KMBombInfo bombInfo) =>
        GetJSONEntries<BatteryJSON>(bombInfo, KMBombInfo.QUERYKEY_GET_BATTERIES, null).Where(x => x != null);

    private static IEnumerable<PortsJSON> GetPortEntries(KMBombInfo bombInfo) =>
        GetJSONEntries<PortsJSON>(bombInfo, KMBombInfo.QUERYKEY_GET_PORTS, null).Where(x => x != null);

    private static IEnumerable<SerialNumberJSON> GetSerialNumberEntries(KMBombInfo bombInfo) =>
        GetJSONEntries<SerialNumberJSON>(bombInfo, KMBombInfo.QUERYKEY_GET_SERIAL_NUMBER, null).Where(x => x != null);

    private static IEnumerable<TwoFactorJSON> GetTwoFactorEntries(KMBombInfo bombInfo) =>
        GetJSONEntries<TwoFactorJSON>(bombInfo, WidgetQueryTwofactor, null).Where(x => x != null);

    #endregion

    #region Public Extensions

    public static bool IsIndicatorPresent(this KMBombInfo bombInfo, Indicator indicatorLabel) =>
        bombInfo.IsIndicatorPresent(indicatorLabel.ToString());

    public static bool IsIndicatorPresent(this KMBombInfo bombInfo, string indicatorLabel) =>
        GetIndicatorEntries(bombInfo).Any(x => indicatorLabel.Equals(x.label));

    public static bool IsIndicatorOn(this KMBombInfo bombInfo, Indicator indicatorLabel) =>
        bombInfo.IsIndicatorOn(indicatorLabel.ToString());

    public static bool IsIndicatorOn(this KMBombInfo bombInfo, string indicatorLabel) =>
        GetIndicatorEntries(bombInfo).Any(x => x.IsOn() && indicatorLabel.Equals(x.label));

    public static bool IsIndicatorOff(this KMBombInfo bombInfo, Indicator indicatorLabel) =>
        bombInfo.IsIndicatorOff(indicatorLabel.ToString());

    public static bool IsIndicatorOff(this KMBombInfo bombInfo, string indicatorLabel) =>
        GetIndicatorEntries(bombInfo).Any(x => !x.IsOn() && indicatorLabel.Equals(x.label));

    public static IEnumerable<string> GetIndicators(this KMBombInfo bombInfo) =>
        GetIndicatorEntries(bombInfo).Select(x => x.label);

    public static IEnumerable<string> GetOnIndicators(this KMBombInfo bombInfo) =>
        GetIndicatorEntries(bombInfo).Where(x => x.IsOn()).Select(x => x.label);

    public static IEnumerable<string> GetOffIndicators(this KMBombInfo bombInfo) =>
        GetIndicatorEntries(bombInfo).Where(x => !x.IsOn()).Select(x => x.label);

    public static int GetBatteryCount(this KMBombInfo bombInfo) =>
        GetBatteryEntries(bombInfo).Sum(x => x.numbatteries);

    public static int GetBatteryCount(this KMBombInfo bombInfo, Battery batteryType) =>
        GetBatteryCount(bombInfo, (int) batteryType);

    public static int GetBatteryCount(this KMBombInfo bombInfo, int batteryType) =>
        GetBatteryEntries(bombInfo).Where(x => x.numbatteries == batteryType).Sum(x => x.numbatteries);

    public static int GetBatteryHolderCount(this KMBombInfo bombInfo) =>
        GetBatteryEntries(bombInfo).Count();

    public static int GetBatteryHolderCount(this KMBombInfo bombInfo, Battery batteryType) =>
        GetBatteryHolderCount(bombInfo, (int) batteryType);

    public static int GetBatteryHolderCount(this KMBombInfo bombInfo, int batteryType) =>
        GetBatteryEntries(bombInfo).Count(x => x.numbatteries == batteryType);

    public static int GetPortCount(this KMBombInfo bombInfo) =>
        GetPortEntries(bombInfo).Sum(x => x.presentPorts.Length);

    public static int GetPortCount(this KMBombInfo bombInfo, Port portType) =>
        bombInfo.GetPortCount(portType.ToString());

    public static int GetPortCount(this KMBombInfo bombInfo, string portType) =>
        GetPortEntries(bombInfo).Sum(x => x.presentPorts.Count(y => portType.Equals(y)));

    public static int GetPortPlateCount(this KMBombInfo bombInfo) =>
        GetPortEntries(bombInfo).Count();

    public static IEnumerable<string> GetPorts(this KMBombInfo bombInfo) =>
        GetPortEntries(bombInfo).SelectMany(x => x.presentPorts);

    public static IEnumerable<string[]> GetPortPlates(this KMBombInfo bombInfo) =>
        GetPortEntries(bombInfo).Where(x => x != null).Select(x => x.presentPorts);

    public static bool IsPortPresent(this KMBombInfo bombInfo, Port portType) =>
        bombInfo.IsPortPresent(portType.ToString());

    public static bool IsPortPresent(this KMBombInfo bombInfo, string portType) =>
        GetPortEntries(bombInfo).Any(x => x.presentPorts != null && x.presentPorts.Any(y => portType.Equals(y)));

    public static int CountUniquePorts(this KMBombInfo bombInfo) =>
        GetPorts(bombInfo).Distinct().Count();

    public static bool IsDuplicatePortPresent(this KMBombInfo bombInfo)
    {
        var ports = new HashSet<string>();
        foreach (var port in GetPorts(bombInfo))
            if (!ports.Add(port))
                return true;
        return false;
    }

    public static bool IsDuplicatePortPresent(this KMBombInfo bombInfo, Port port) =>
        IsDuplicatePortPresent(bombInfo, port.ToString());

    public static bool IsDuplicatePortPresent(this KMBombInfo bombInfo, string port) =>
        GetPortCount(bombInfo, port) > 1;

    public static string GetSerialNumber(this KMBombInfo bombInfo) =>
        GetSerialNumberEntries(bombInfo).FirstOrDefault()?.serial;

    public static IEnumerable<char> GetSerialNumberLetters(this KMBombInfo bombInfo) =>
        GetSerialNumber(bombInfo).Where(x => x is < '0' or > '9');

    public static IEnumerable<int> GetSerialNumberNumbers(this KMBombInfo bombInfo) =>
        GetSerialNumber(bombInfo).Where(x => x is >= '0' and <= '9').Select(y => y - '0');

    public static bool IsTwoFactorPresent(this KMBombInfo bombInfo) =>
        GetTwoFactorCodes(bombInfo).Any();

    public static int GetTwoFactorCounts(this KMBombInfo bombInfo) =>
        GetTwoFactorCodes(bombInfo).Count();

    public static IEnumerable<int> GetTwoFactorCodes(this KMBombInfo bombInfo) =>
        GetTwoFactorEntries(bombInfo).Select(x => x.twofactor_key);
    #endregion
}
