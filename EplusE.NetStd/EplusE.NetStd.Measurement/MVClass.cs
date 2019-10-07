namespace EplusE.Measurement
{
    /// <summary>
    /// Measurand class, i.e. Temperature, Humidity, Velocity, CO2
    /// </summary>
    public enum MVClass : byte
    {
        /// <summary>
        /// invalid
        /// </summary>
        INVALID = 0,

        /// <summary>
        /// i.e. global setting (all classes)
        /// </summary>
        GLOBAL,

        /// <summary>
        /// Temperature [°C, °F, K]
        /// </summary>
        Temperature,

        /// <summary>
        /// RH [%rH]
        /// </summary>
        RelativeHumidity,

        /// <summary>
        /// (Air) Velocity/Flow [m/s, ft/min]
        /// </summary>
        Velocity,

        /// <summary>
        /// Standardized (Air) Velocity/Flow [m/s, ft/min]
        /// </summary>
        StdVelocity,

        /// <summary>
        /// CO2 gas concentration (Mean) [ppm]
        /// </summary>
        CO2,

        /// <summary>
        /// CO2 gas concentration (Raw) [ppm]
        /// </summary>
        CO2Raw,

        /// <summary>
        /// O2 (oxygen) gas concentration (Mean) [%]
        /// </summary>
        O2,

        /// <summary>
        /// Water vapor partial pressure e [mbar, psi]
        /// </summary>
        WaterVaporPartialPressure,

        /// <summary>
        /// Dew point temperature Td [°C, °F]
        /// </summary>
        DewPoint,

        /// <summary>
        /// Wet bulb temperature Tw [°C, °F]
        /// </summary>
        WetBulb,

        /// <summary>
        /// Absolute humidity dv [g/m3, gr/ft3]
        /// </summary>
        AbsoluteHumidity,

        /// <summary>
        /// Mixing ratio r [g/kg, gr/lb]
        /// </summary>
        MixingRatio,

        /// <summary>
        /// Enthalpy h [kJ/kg, kJ/lb]
        /// </summary>
        Enthalpy,

        /// <summary>
        /// Td or Tf [°C, °F]
        /// </summary>
        DewPointOrFrostPoint,

        /// <summary>
        /// Water activity Aw [1]
        /// </summary>
        WaterActivity,

        /// <summary>
        /// Percent saturation [%], PALL measurand for relative humidity [0..100 %rH]
        /// </summary>
        PercentSaturation,

        /// <summary>
        /// Volume concentration Wv [ppm]
        /// </summary>
        VolumeConcentration,

        /// <summary>
        /// Water content X [ppm]
        /// </summary>
        WaterContent,

        /// <summary>
        /// Specific mass flow [kg/sm2]
        /// </summary>
        SpecificMassFlow,

        /// <summary>
        /// Mass flow [kg/h, kg/min, kg/s]
        /// </summary>
        MassFlow,

        /// <summary>
        /// Standardized volumetric flow [m3/h, m3/min, SLPM, l/min, l/s, ft3/min, m3/s]
        /// </summary>
        StdVolumetricFlow,

        /// <summary>
        /// Volumetric flow [m3/min, ft3/min]
        /// </summary>
        VolumetricFlow,

        /// <summary>
        /// Volumetric consumption (resetable) [m3, ft3]
        /// </summary>
        VolumetricConsumption,

        /// <summary>
        /// Volumetric consumption (total) [m3, ft3]
        /// </summary>
        VolumetricConsumptionTotal,

        /// <summary>
        /// (Air) Pressure [mbar, psi, bar]
        /// </summary>
        Pressure,

        /// <summary>
        /// Resistance [Ohm]
        /// </summary>
        Resistance,

        /// <summary>
        /// Current [mA]
        /// </summary>
        Current,

        /// <summary>
        /// Frequency [kHz]
        /// </summary>
        Frequency,

        /// <summary>
        /// Voltage [V]
        /// </summary>
        Voltage,

        /// <summary>
        /// Diameter [mm]
        /// </summary>
        Diameter,

        /// <summary>
        /// Filter contamination [%]
        /// </summary>
        FilterContamination,

        /// <summary>
        /// Level indicator [cm, inch]
        /// </summary>
        LevelIndicator,

        /// <summary>
        /// Differential pressure [mbar, mmH2O, inH2O]
        /// </summary>
        DifferentialPressure
    }
}