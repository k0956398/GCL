namespace EplusE.Measurement
{
    /// <summary>
    /// Measurement value codes (coordinated with E2Bus MV codes!)
    /// </summary>
    public enum MVCode : byte
    {
        /// <summary>
        /// invalid
        /// </summary>
        INVALID = 0,

        /// <summary>
        /// Temp. [°C]
        /// </summary>
        TEMP__DEG_C = 1,

        /// <summary>
        /// Temp. [°F]
        /// </summary>
        TEMP__DEG_F = 2,

        /// <summary>
        /// Temp. [K]
        /// </summary>
        TEMP__DEG_K = 4,

        /// <summary>
        /// RH [%rH]
        /// </summary>
        RH__PCT = 10,

        /// <summary>
        /// (Air) Velocity/Flow v [m/s]
        /// </summary>
        V__M_PER_SEC = 20,

        /// <summary>
        /// (Air) Velocity/Flow v [ft/min]
        /// </summary>
        V__FT_PER_MIN = 21,

        /// <summary>
        /// Standardized (Air) Velocity/Flow v [m/s]
        /// </summary>
        StdV__M_PER_SEC = 22,

        /// <summary>
        /// Standardized (Air) Velocity/Flow v [ft/min]
        /// </summary>
        StdV__FT_PER_MIN = 23,

        /// <summary>
        /// Volumetric flow V' [m³/h]
        /// </summary>
        VolFl__M3_HOUR = 27,

        /// <summary>
        /// Volumetric flow V' [l/s]
        /// </summary>
        VolFl__L_SEC = 28,

        /// <summary>
        /// Volumetric flow V' [m³/s]
        /// </summary>
        VolFl__M3_SEC = 29,

        /// <summary>
        /// CO2 mean value (Median 11) [ppm]
        /// </summary>
        CO2_MEAN__PPM = 30,

        /// <summary>
        /// CO2 raw [ppm]
        /// </summary>
        CO2_RAW__PPM = 31,

        /// <summary>
        /// Filter contamination [%]
        /// </summary>
        Filter__PCT = 37,

        /// <summary>
        /// Level indicator [cm]
        /// </summary>
        Level__CM = 38,

        /// <summary>
        /// Level indicator [inch]
        /// </summary>
        Level__IN = 39,

        /// <summary>
        /// O2 (oxygen) concentration mean value [%]
        /// </summary>
        O2_MEAN__PCT = 40,

        /// <summary>
        /// Water vapor partial pressure e [mbar]
        /// </summary>
        e__MBAR = 50,

        /// <summary>
        /// Water vapor partial pressure e [psi]
        /// </summary>
        e__PSI = 51,

        /// <summary>
        /// Dew point temperature Td [°C]
        /// </summary>
        Td__DEG_C = 52,

        /// <summary>
        /// Dew point temperature Td [°F]
        /// </summary>
        Td__DEG_F = 53,

        /// <summary>
        /// Wet bulb temperature Tw [°C]
        /// </summary>
        Tw__DEG_C = 54,

        /// <summary>
        /// Wet bulb temperature Tw [°F]
        /// </summary>
        Tw__DEG_F = 55,

        /// <summary>
        /// Absolute humidity dv [g/m3]
        /// </summary>
        dv__G_M3 = 56,

        /// <summary>
        /// Absolute humidity dv [gr/ft3]
        /// </summary>
        dv__GR_FT3 = 57,

        /// <summary>
        /// Mixing ratio r [g/kg]
        /// </summary>
        r__G_KG = 60,

        /// <summary>
        /// Mixing ratio r [gr/lb]
        /// </summary>
        r__GR_LB = 61,

        /// <summary>
        /// Enthalpy h [kJ/kg]
        /// </summary>
        h__KJ_KG = 62,

        /// <summary>
        /// Enthalpy h [ft lbf/lb] ([kJ/lb] was documentation error)
        /// </summary>
        h__FT_LBF_LB = 63,

        /// <summary>
        /// Enthalpy h [BTU/lb]
        /// </summary>
        h__BTU_LB = 64,

        /// <summary>
        /// Dew point/frost point temperature Td/Tf [°C]
        /// </summary>
        TdTf__DEG_C = 65,

        /// <summary>
        /// Dew point/frost point temperature Td/Tf [°F]
        /// </summary>
        TdTf__DEG_F = 66,

        /// <summary>
        /// Water activity Aw [1]
        /// </summary>
        Aw__1 = 67,

        /// <summary>
        /// Saturation %S [%]
		/// = Relative humidity, but without unit (empty string)
		///   EE31 Idx = 16
        /// </summary>
        PctS__PCT = 68,

        /// <summary>
        /// Water content X [ppm]
        /// </summary>
        X__PPM = 70,

        /// <summary>
        /// Wet bulb temperature Tw [K]
        /// </summary>
        Tw__DEG_K = 72,

        /// <summary>
        /// Dew point temperature Td [K]
        /// </summary>
        Td__DEG_K = 73,

        /// <summary>
        /// Dew point/frost point temperature Td/Tf [K]
        /// </summary>
        TdTf__DEG_K = 74,

        /// <summary>
        /// Volume concentration (Humidity!) [ppm]
        /// </summary>
        Wv__PPM = 75,

        /// <summary>
        /// Specific mass flow M [kg/sm2]
        /// </summary>
        SpecMassFl__KG_SM2 = 79,

        /// <summary>
        /// Mass flow m [kg/h]
        /// </summary>
        MassFl__KG_H = 80,

        /// <summary>
        /// Mass flow m [kg/min]
        /// </summary>
        MassFl__KG_MIN = 81,

        /// <summary>
        /// Mass flow m [kg/s]
        /// </summary>
        MassFl__KG_SEC = 82,

        /// <summary>
        /// Standardized volumetric flow V0' [m3/h]
        /// </summary>
        StdVolFl__M3_H = 83,

        /// <summary>
        /// Standardized volumetric flow V0' [m3/min]
        /// </summary>
        StdVolFl__M3_MIN = 84,

        /// <summary>
        /// Standardized volumetric flow V0' [l/min = SLPM]
        /// </summary>
        StdVolFl__L_MIN = 85,

        /// <summary>
        /// Standardized volumetric flow V0' [l/s]
        /// </summary>
        StdVolFl__L_SEC = 86,

        /// <summary>
        /// Standardized volumetric flow V0' [SCFM = ft3/min]
        /// </summary>
        StdVolFl__FT3_MIN = 87,

        /// <summary>
        /// Standardized volumetric flow V0' [m3/s]
        /// </summary>
        StdVolFl__M3_SEC = 88,

        /// <summary>
        /// Volumetric flow V' [m3/min]
        /// </summary>
        VolFl__M3_MIN = 89,

        /// <summary>
        /// Volumetric flow V' [ft3/min]
        /// </summary>
        VolFl__FT3_MIN = 90,

        /// <summary>
        /// Volumetric consumption (resetable) Q0 [m3]
        /// </summary>
        VolConsump__M3 = 91,

        /// <summary>
        /// Volumetric consumption (resetable) Q0 [ft3]
        /// </summary>
        VolConsump__FT3 = 93,

        /// <summary>
        /// Volumetric consumption (total) Q0 [m3]
        /// </summary>
        VolConsumpTotal__M3 = 95,

        /// <summary>
        /// Volumetric consumption (total) Q0 [ft3]
        /// </summary>
        VolConsumpTotal__FT3 = 97,

        /// <summary>
        /// (Air) Pressure [mbar]
        /// </summary>
        Pressure__MBAR = 100,

        /// <summary>
        /// (Air) Pressure [psi]
        /// </summary>
        Pressure__PSI = 101,

        /// <summary>
        /// (Air) Pressure [bar]
        /// </summary>
        Pressure__BAR = 104,

        /// <summary>
        /// Differential pressure [mmH2O]
        /// </summary>
        DiffPressure__MMH2O = 105,

        /// <summary>
        /// Differential pressure [mbar]
        /// </summary>
        DiffPressure__MBAR = 106,

        /// <summary>
        /// Differential pressure [inchH2O]
        /// </summary>
        DiffPressure__INH2O = 109,

        /// <summary>
        /// Resistance [Ohm]
        /// </summary>
        Resistance__OHM = 132,

        /// <summary>
        /// Current I [mA]
        /// </summary>
        Current__MA = 154,

        /// <summary>
        /// Frequency [kHz]
        /// </summary>
        Frequency__KHZ = 160,

        /// <summary>
        /// Voltage V [V]
        /// </summary>
        Voltage__V = 173,

        /// <summary>
        /// Diameter [mm]
        /// </summary>
        Diameter__MM = 192
    }
}