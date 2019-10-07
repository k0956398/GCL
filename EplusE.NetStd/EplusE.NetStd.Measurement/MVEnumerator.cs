using System.Collections.Generic;
using System.Linq;

namespace EplusE.Measurement
{
    /// <summary>
    /// Measurement value enumerator class
    /// </summary>
    public static class MVEnumerator
    {
        private static readonly MVCode[] MembersAbsoluteHumidity = { MVCode.dv__G_M3, MVCode.dv__GR_FT3 };
        private static readonly MVCode[] MembersCO2 = { MVCode.CO2_MEAN__PPM };
        private static readonly MVCode[] MembersCO2Raw = { MVCode.CO2_RAW__PPM };
        private static readonly MVCode[] MembersCurrent = { MVCode.Current__MA };
        private static readonly MVCode[] MembersDewPoint = { MVCode.Td__DEG_C, MVCode.Td__DEG_F, MVCode.Td__DEG_K };
        private static readonly MVCode[] MembersDewPointOrFrostPoint = { MVCode.TdTf__DEG_C, MVCode.TdTf__DEG_F, MVCode.TdTf__DEG_K };
        private static readonly MVCode[] MembersDiameter = { MVCode.Diameter__MM };
        private static readonly MVCode[] MembersDifferentialPressure = { MVCode.DiffPressure__MBAR, MVCode.DiffPressure__MMH2O, MVCode.DiffPressure__INH2O };
        private static readonly MVCode[] MembersEnthalpy = { MVCode.h__KJ_KG, MVCode.h__FT_LBF_LB, MVCode.h__BTU_LB };
        private static readonly MVCode[] MembersFilterContamination = { MVCode.Filter__PCT };
        private static readonly MVCode[] MembersFrequency = { MVCode.Frequency__KHZ };
        private static readonly MVCode[] MembersLevelIndicator = { MVCode.Level__CM, MVCode.Level__IN };
        private static readonly MVCode[] MembersMassFlow = { MVCode.MassFl__KG_H, MVCode.MassFl__KG_MIN, MVCode.MassFl__KG_SEC };
        private static readonly MVCode[] MembersMixingRatio = { MVCode.r__G_KG, MVCode.r__GR_LB };
        private static readonly MVCode[] MembersO2 = { MVCode.O2_MEAN__PCT };
        private static readonly MVCode[] MembersPercentSaturation = { MVCode.PctS__PCT };
        private static readonly MVCode[] MembersPressure = { MVCode.Pressure__MBAR, MVCode.Pressure__PSI, MVCode.Pressure__BAR };
        private static readonly MVCode[] MembersRelativeHumidity = { MVCode.RH__PCT };
        private static readonly MVCode[] MembersResistance = { MVCode.Resistance__OHM };
        private static readonly MVCode[] MembersSpecificMassFlow = { MVCode.SpecMassFl__KG_SM2 };
        private static readonly MVCode[] MembersStdVelocity = { MVCode.StdV__M_PER_SEC, MVCode.StdV__FT_PER_MIN };
        private static readonly MVCode[] MembersStdVolumetricFlow = { MVCode.StdVolFl__M3_H, MVCode.StdVolFl__M3_MIN, MVCode.StdVolFl__L_MIN, MVCode.StdVolFl__L_SEC, MVCode.StdVolFl__FT3_MIN, MVCode.StdVolFl__M3_SEC };
        private static readonly MVCode[] MembersTemperature = { MVCode.TEMP__DEG_C, MVCode.TEMP__DEG_F, MVCode.TEMP__DEG_K };

        private static readonly MVCode[] MembersUnitNeutral =
            { MVCode.TEMP__DEG_K, MVCode.Td__DEG_K, MVCode.Tw__DEG_K, MVCode.TdTf__DEG_K, MVCode.RH__PCT, MVCode.CO2_MEAN__PPM, MVCode.CO2_RAW__PPM,
              MVCode.O2_MEAN__PCT, MVCode.Aw__1, MVCode.PctS__PCT, MVCode.Wv__PPM, MVCode.X__PPM,
              MVCode.SpecMassFl__KG_SM2, MVCode.MassFl__KG_H, MVCode.MassFl__KG_MIN, MVCode.MassFl__KG_SEC, MVCode.StdVolFl__M3_H,
              MVCode.StdVolFl__L_MIN, MVCode.StdVolFl__L_SEC, MVCode.StdVolFl__M3_SEC, MVCode.Resistance__OHM, MVCode.Current__MA,
              MVCode.Frequency__KHZ, MVCode.Voltage__V, MVCode.Diameter__MM, MVCode.VolFl__M3_HOUR, MVCode.VolFl__L_SEC, MVCode.VolFl__M3_SEC,
              MVCode.Filter__PCT, MVCode.DiffPressure__MBAR };

        // NOTE: Keep MembersUnitSI and MembersUnitUS in sync, which means same order/index position to enable SI/US switches
        private static readonly MVCode[] MembersUnitSI =
            { MVCode.TEMP__DEG_C, MVCode.V__M_PER_SEC, MVCode.StdV__M_PER_SEC, MVCode.e__MBAR, MVCode.Td__DEG_C, MVCode.Tw__DEG_C,
              MVCode.dv__G_M3, MVCode.r__G_KG, MVCode.h__KJ_KG, MVCode.h__KJ_KG, MVCode.TdTf__DEG_C,
              MVCode.StdVolFl__M3_MIN, MVCode.VolFl__M3_MIN, MVCode.VolConsump__M3, MVCode.VolConsumpTotal__M3, MVCode.Pressure__MBAR, MVCode.Pressure__BAR,
              MVCode.Level__CM, MVCode.DiffPressure__MMH2O };

        private static readonly MVCode[] MembersUnitUS =
            { MVCode.TEMP__DEG_F, MVCode.V__FT_PER_MIN, MVCode.StdV__FT_PER_MIN, MVCode.e__PSI, MVCode.Td__DEG_F, MVCode.Tw__DEG_F,
              MVCode.dv__GR_FT3, MVCode.r__GR_LB, MVCode.h__FT_LBF_LB, MVCode.h__BTU_LB, MVCode.TdTf__DEG_F,
              MVCode.StdVolFl__FT3_MIN, MVCode.VolFl__FT3_MIN, MVCode.VolConsump__FT3, MVCode.VolConsumpTotal__FT3, MVCode.Pressure__PSI, MVCode.Pressure__PSI,
              MVCode.Level__IN, MVCode.DiffPressure__INH2O };

        private static readonly MVCode[] MembersVelocity = { MVCode.V__M_PER_SEC, MVCode.V__FT_PER_MIN };
        private static readonly MVCode[] MembersVoltage = { MVCode.Voltage__V };
        private static readonly MVCode[] MembersVolumeConcentration = { MVCode.Wv__PPM };
        private static readonly MVCode[] MembersVolumetricConsumption = { MVCode.VolConsump__M3, MVCode.VolConsump__FT3 };
        private static readonly MVCode[] MembersVolumetricConsumptionTotal = { MVCode.VolConsumpTotal__M3, MVCode.VolConsumpTotal__FT3 };
        private static readonly MVCode[] MembersVolumetricFlow = { MVCode.VolFl__M3_MIN, MVCode.VolFl__FT3_MIN, MVCode.VolFl__M3_HOUR, MVCode.VolFl__L_SEC, MVCode.VolFl__M3_SEC };
        private static readonly MVCode[] MembersWaterActivity = { MVCode.Aw__1 };
        private static readonly MVCode[] MembersWaterContent = { MVCode.X__PPM };
        private static readonly MVCode[] MembersWaterVaporPartialPressure = { MVCode.e__MBAR, MVCode.e__PSI };
        private static readonly MVCode[] MembersWetBulb = { MVCode.Tw__DEG_C, MVCode.Tw__DEG_F, MVCode.Tw__DEG_K };

        /// <summary>
        /// Ensures measurement value is of the unit system SI.
        /// </summary>
        /// <param name="mvCode">The measurement value code.</param>
        /// <returns>Measurement value code that is of unit system SI (may be the same as parameter <paramref name="mvCode"/>).</returns>
        public static MVCode EnsureUnitSystemSI(MVCode mvCode)
        {
            if (MVCode.INVALID == mvCode)
                return mvCode;
            if (MVCode.TEMP__DEG_K == mvCode)
                return MVCode.TEMP__DEG_C;
            if (IsUnitSystemSIorNeutral(mvCode))
                return mvCode;
            return MembersUnitSI.ElementAt(MembersUnitUS.ToList().FindIndex(mv => mv.Equals(mvCode)));
        }

        /// <summary>
        /// Ensures measurement value is of the unit system US.
        /// </summary>
        /// <param name="mvCode">The measurement value code.</param>
        /// <returns>Measurement value code that is of unit system US (may be the same as parameter <paramref name="mvCode"/>).</returns>
        public static MVCode EnsureUnitSystemUS(MVCode mvCode)
        {
            if (MVCode.INVALID == mvCode)
                return mvCode;
            if (MVCode.TEMP__DEG_K == mvCode)
                return MVCode.TEMP__DEG_F;
            if (IsUnitSystemUSorNeutral(mvCode))
                return mvCode;
            return MembersUnitUS.ElementAt(MembersUnitSI.ToList().FindIndex(mv => mv.Equals(mvCode)));
        }

        /// <summary>
        /// Gets the measurement value class from a measurement value code.
        /// </summary>
        /// <param name="mvCode">The measurement value code.</param>
        /// <returns></returns>
        public static MVClass GetClass(MVCode mvCode)
        {
            if (Enumerable.Contains(MembersTemperature, mvCode))
                return MVClass.Temperature;
            if (Enumerable.Contains(MembersRelativeHumidity, mvCode))
                return MVClass.RelativeHumidity;
            if (Enumerable.Contains(MembersVelocity, mvCode))
                return MVClass.Velocity;
            if (Enumerable.Contains(MembersStdVelocity, mvCode))
                return MVClass.StdVelocity;
            if (Enumerable.Contains(MembersCO2, mvCode))
                return MVClass.CO2;
            if (Enumerable.Contains(MembersCO2Raw, mvCode))
                return MVClass.CO2Raw;
            if (Enumerable.Contains(MembersO2, mvCode))
                return MVClass.O2;
            if (Enumerable.Contains(MembersWaterVaporPartialPressure, mvCode))
                return MVClass.WaterVaporPartialPressure;
            if (Enumerable.Contains(MembersDewPoint, mvCode))
                return MVClass.DewPoint;
            if (Enumerable.Contains(MembersWetBulb, mvCode))
                return MVClass.WetBulb;
            if (Enumerable.Contains(MembersAbsoluteHumidity, mvCode))
                return MVClass.AbsoluteHumidity;
            if (Enumerable.Contains(MembersMixingRatio, mvCode))
                return MVClass.MixingRatio;
            if (Enumerable.Contains(MembersEnthalpy, mvCode))
                return MVClass.Enthalpy;
            if (Enumerable.Contains(MembersDewPointOrFrostPoint, mvCode))
                return MVClass.DewPointOrFrostPoint;
            if (Enumerable.Contains(MembersWaterActivity, mvCode))
                return MVClass.WaterActivity;
            if (Enumerable.Contains(MembersPercentSaturation, mvCode))
                return MVClass.PercentSaturation;
            if (Enumerable.Contains(MembersVolumeConcentration, mvCode))
                return MVClass.VolumeConcentration;
            if (Enumerable.Contains(MembersWaterContent, mvCode))
                return MVClass.WaterContent;
            if (Enumerable.Contains(MembersSpecificMassFlow, mvCode))
                return MVClass.SpecificMassFlow;
            if (Enumerable.Contains(MembersMassFlow, mvCode))
                return MVClass.MassFlow;
            if (Enumerable.Contains(MembersStdVolumetricFlow, mvCode))
                return MVClass.StdVolumetricFlow;
            if (Enumerable.Contains(MembersVolumetricFlow, mvCode))
                return MVClass.VolumetricFlow;
            if (Enumerable.Contains(MembersVolumetricConsumption, mvCode))
                return MVClass.VolumetricConsumption;
            if (Enumerable.Contains(MembersVolumetricConsumptionTotal, mvCode))
                return MVClass.VolumetricConsumptionTotal;
            if (Enumerable.Contains(MembersPressure, mvCode))
                return MVClass.Pressure;
            if (Enumerable.Contains(MembersResistance, mvCode))
                return MVClass.Resistance;
            if (Enumerable.Contains(MembersCurrent, mvCode))
                return MVClass.Current;
            if (Enumerable.Contains(MembersFrequency, mvCode))
                return MVClass.Frequency;
            if (Enumerable.Contains(MembersVoltage, mvCode))
                return MVClass.Voltage;
            if (Enumerable.Contains(MembersDiameter, mvCode))
                return MVClass.Diameter;
            if (Enumerable.Contains(MembersFilterContamination, mvCode))
                return MVClass.FilterContamination;
            if (Enumerable.Contains(MembersLevelIndicator, mvCode))
                return MVClass.LevelIndicator;
            if (Enumerable.Contains(MembersDifferentialPressure, mvCode))
                return MVClass.DifferentialPressure;
            return MVClass.INVALID;
        }

        /// <summary>
        /// Gets the codes of measurement value class.
        /// </summary>
        /// <param name="mvClass">The measurement value class.</param>
        /// <returns></returns>
        public static IEnumerator<MVCode> GetCodesOfClass(MVClass mvClass)
        {
            switch (mvClass)
            {
                case MVClass.INVALID:
                    yield return MVCode.INVALID;
                    break;

                case MVClass.Temperature:
                    foreach (MVCode mvCode in MembersTemperature)
                        yield return mvCode;
                    break;

                case MVClass.RelativeHumidity:
                    foreach (MVCode mvCode in MembersRelativeHumidity)
                        yield return mvCode;
                    break;

                case MVClass.Velocity:
                    foreach (MVCode mvCode in MembersVelocity)
                        yield return mvCode;
                    break;

                case MVClass.StdVelocity:
                    foreach (MVCode mvCode in MembersStdVelocity)
                        yield return mvCode;
                    break;

                case MVClass.CO2:
                    foreach (MVCode mvCode in MembersCO2)
                        yield return mvCode;
                    break;

                case MVClass.CO2Raw:
                    foreach (MVCode mvCode in MembersCO2Raw)
                        yield return mvCode;
                    break;

                case MVClass.O2:
                    foreach (MVCode mvCode in MembersO2)
                        yield return mvCode;
                    break;

                case MVClass.WaterVaporPartialPressure:
                    foreach (MVCode mvCode in MembersWaterVaporPartialPressure)
                        yield return mvCode;
                    break;

                case MVClass.DewPoint:
                    foreach (MVCode mvCode in MembersDewPoint)
                        yield return mvCode;
                    break;

                case MVClass.WetBulb:
                    foreach (MVCode mvCode in MembersWetBulb)
                        yield return mvCode;
                    break;

                case MVClass.AbsoluteHumidity:
                    foreach (MVCode mvCode in MembersAbsoluteHumidity)
                        yield return mvCode;
                    break;

                case MVClass.MixingRatio:
                    foreach (MVCode mvCode in MembersMixingRatio)
                        yield return mvCode;
                    break;

                case MVClass.Enthalpy:
                    foreach (MVCode mvCode in MembersEnthalpy)
                        yield return mvCode;
                    break;

                case MVClass.DewPointOrFrostPoint:
                    foreach (MVCode mvCode in MembersDewPointOrFrostPoint)
                        yield return mvCode;
                    break;

                case MVClass.WaterActivity:
                    foreach (MVCode mvCode in MembersWaterActivity)
                        yield return mvCode;
                    break;

                case MVClass.PercentSaturation:
                    foreach (MVCode mvCode in MembersPercentSaturation)
                        yield return mvCode;
                    break;

                case MVClass.VolumeConcentration:
                    foreach (MVCode mvCode in MembersVolumeConcentration)
                        yield return mvCode;
                    break;

                case MVClass.WaterContent:
                    foreach (MVCode mvCode in MembersWaterContent)
                        yield return mvCode;
                    break;

                case MVClass.SpecificMassFlow:
                    foreach (MVCode mvCode in MembersSpecificMassFlow)
                        yield return mvCode;
                    break;

                case MVClass.MassFlow:
                    foreach (MVCode mvCode in MembersMassFlow)
                        yield return mvCode;
                    break;

                case MVClass.StdVolumetricFlow:
                    foreach (MVCode mvCode in MembersStdVolumetricFlow)
                        yield return mvCode;
                    break;

                case MVClass.VolumetricFlow:
                    foreach (MVCode mvCode in MembersVolumetricFlow)
                        yield return mvCode;
                    break;

                case MVClass.VolumetricConsumption:
                    foreach (MVCode mvCode in MembersVolumetricConsumption)
                        yield return mvCode;
                    break;

                case MVClass.VolumetricConsumptionTotal:
                    foreach (MVCode mvCode in MembersVolumetricConsumptionTotal)
                        yield return mvCode;
                    break;

                case MVClass.Pressure:
                    foreach (MVCode mvCode in MembersPressure)
                        yield return mvCode;
                    break;

                case MVClass.Resistance:
                    foreach (MVCode mvCode in MembersResistance)
                        yield return mvCode;
                    break;

                case MVClass.Current:
                    foreach (MVCode mvCode in MembersCurrent)
                        yield return mvCode;
                    break;

                case MVClass.Frequency:
                    foreach (MVCode mvCode in MembersFrequency)
                        yield return mvCode;
                    break;

                case MVClass.Voltage:
                    foreach (MVCode mvCode in MembersVoltage)
                        yield return mvCode;
                    break;

                case MVClass.Diameter:
                    foreach (MVCode mvCode in MembersDiameter)
                        yield return mvCode;
                    break;

                case MVClass.FilterContamination:
                    foreach (MVCode mvCode in MembersFilterContamination)
                        yield return mvCode;
                    break;

                case MVClass.LevelIndicator:
                    foreach (MVCode mvCode in MembersLevelIndicator)
                        yield return mvCode;
                    break;

                case MVClass.DifferentialPressure:
                    foreach (MVCode mvCode in MembersDifferentialPressure)
                        yield return mvCode;
                    break;
            }
        }

        /// <summary>
        /// Gets the first code of measurement value class.
        /// </summary>
        /// <param name="mvClass">The measurement value class.</param>
        /// <returns></returns>
        public static MVCode GetFirstCodeOfClass(MVClass mvClass)
        {
            IEnumerator<MVCode> it = GetCodesOfClass(mvClass);
            if (null == it)
                return MVCode.INVALID;
            it.MoveNext();
            return it.Current;
        }

        /// <summary>
        /// Gets the (engineering) unit system, i.e. SI or US (or undefined for neutral MV codes).
        /// </summary>
        /// <param name="mvCode">The measurement value code.</param>
        /// <returns></returns>
        public static SIUSUnit GetUnitSystem(MVCode mvCode)
        {
            if (Enumerable.Contains(MembersUnitSI, mvCode))
                return SIUSUnit.SI;
            if (Enumerable.Contains(MembersUnitUS, mvCode))
                return SIUSUnit.US;
            return SIUSUnit.Undefined;
        }

        /// <summary>
        /// Determines whether measurement value code 1 is from different class as the specified measurement value code 2.
        /// </summary>
        /// <param name="mvCode1">The measurement value code 1.</param>
        /// <param name="mvCode2">The measurement value code 2.</param>
        /// <returns>
        ///   <c>true</c> if they are from different measurement value classes; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDifferentClass(MVCode mvCode1, MVCode mvCode2)
        {
            return (GetClass(mvCode1) != GetClass(mvCode2));
        }

        /// <summary>
        /// Determines whether measurement value code 1 is from different unit system as the specified measurement value code 2.
        /// </summary>
        /// <param name="mvCode1">The measurement value code 1.</param>
        /// <param name="mvCode2">The measurement value code 2.</param>
        /// <returns>
        ///   <c>true</c> if they are from different unit systems; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDifferentUnitSystem(MVCode mvCode1, MVCode mvCode2)
        {
            //return (IsUnitSystemSI(mvCode1) && IsUnitSystemUS(mvCode2)) ||
            //       (IsUnitSystemUS(mvCode1) && IsUnitSystemSI(mvCode2));
            SIUSUnit unit1 = GetUnitSystem(mvCode1);
            SIUSUnit unit2 = GetUnitSystem(mvCode2);
            return (unit1 != unit2);
        }

        /// <summary>
        /// Determines whether measurement value code is member of the specified mv class.
        /// </summary>
        /// <param name="mvCode">The measurement value code.</param>
        /// <param name="mvClass">The measurement value class.</param>
        /// <returns>
        ///   <c>true</c> if is member of [the specified measurement value class; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsMemberOf(MVCode mvCode, MVClass mvClass)
        {
            switch (mvClass)
            {
                case MVClass.Temperature:
                    return Enumerable.Contains(MembersTemperature, mvCode);

                case MVClass.RelativeHumidity:
                    return Enumerable.Contains(MembersRelativeHumidity, mvCode);

                case MVClass.Velocity:
                    return Enumerable.Contains(MembersVelocity, mvCode);

                case MVClass.StdVelocity:
                    return Enumerable.Contains(MembersStdVelocity, mvCode);

                case MVClass.CO2:
                    return Enumerable.Contains(MembersCO2, mvCode);

                case MVClass.CO2Raw:
                    return Enumerable.Contains(MembersCO2Raw, mvCode);

                case MVClass.O2:
                    return Enumerable.Contains(MembersO2, mvCode);

                case MVClass.WaterVaporPartialPressure:
                    return Enumerable.Contains(MembersWaterVaporPartialPressure, mvCode);

                case MVClass.DewPoint:
                    return Enumerable.Contains(MembersDewPoint, mvCode);

                case MVClass.WetBulb:
                    return Enumerable.Contains(MembersWetBulb, mvCode);

                case MVClass.AbsoluteHumidity:
                    return Enumerable.Contains(MembersAbsoluteHumidity, mvCode);

                case MVClass.MixingRatio:
                    return Enumerable.Contains(MembersMixingRatio, mvCode);

                case MVClass.Enthalpy:
                    return Enumerable.Contains(MembersEnthalpy, mvCode);

                case MVClass.DewPointOrFrostPoint:
                    return Enumerable.Contains(MembersDewPointOrFrostPoint, mvCode);

                case MVClass.WaterActivity:
                    return Enumerable.Contains(MembersWaterActivity, mvCode);

                case MVClass.PercentSaturation:
                    return Enumerable.Contains(MembersPercentSaturation, mvCode);

                case MVClass.VolumeConcentration:
                    return Enumerable.Contains(MembersVolumeConcentration, mvCode);

                case MVClass.WaterContent:
                    return Enumerable.Contains(MembersWaterContent, mvCode);

                case MVClass.SpecificMassFlow:
                    return Enumerable.Contains(MembersSpecificMassFlow, mvCode);

                case MVClass.MassFlow:
                    return Enumerable.Contains(MembersMassFlow, mvCode);

                case MVClass.StdVolumetricFlow:
                    return Enumerable.Contains(MembersStdVolumetricFlow, mvCode);

                case MVClass.VolumetricFlow:
                    return Enumerable.Contains(MembersVolumetricFlow, mvCode);

                case MVClass.VolumetricConsumption:
                    return Enumerable.Contains(MembersVolumetricConsumption, mvCode);

                case MVClass.VolumetricConsumptionTotal:
                    return Enumerable.Contains(MembersVolumetricConsumptionTotal, mvCode);

                case MVClass.Pressure:
                    return Enumerable.Contains(MembersPressure, mvCode);

                case MVClass.Resistance:
                    return Enumerable.Contains(MembersResistance, mvCode);

                case MVClass.Current:
                    return Enumerable.Contains(MembersCurrent, mvCode);

                case MVClass.Frequency:
                    return Enumerable.Contains(MembersFrequency, mvCode);

                case MVClass.Voltage:
                    return Enumerable.Contains(MembersVoltage, mvCode);

                case MVClass.Diameter:
                    return Enumerable.Contains(MembersDiameter, mvCode);

                case MVClass.FilterContamination:
                    return Enumerable.Contains(MembersFilterContamination, mvCode);

                case MVClass.LevelIndicator:
                    return Enumerable.Contains(MembersLevelIndicator, mvCode);

                case MVClass.DifferentialPressure:
                    return Enumerable.Contains(MembersDifferentialPressure, mvCode);
            }
            return false;
        }

        /// <summary>
        /// Determines whether measurement value code is unit system neutral.
        /// </summary>
        /// <param name="mvCode">The measurement value code.</param>
        /// <returns>
        ///   <c>true</c> if it is unit system neutral; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUnitSystemNeutral(MVCode mvCode)
        {
            return Enumerable.Contains(MembersUnitNeutral, mvCode);
        }

        /// <summary>
        /// Determines whether measurement value code is of unit system SI.
        /// </summary>
        /// <param name="mvCode">The measurement value code.</param>
        /// <returns>
        ///   <c>true</c> if it is of unit system SI; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUnitSystemSI(MVCode mvCode)
        {
            return Enumerable.Contains(MembersUnitSI, mvCode);
        }

        /// <summary>
        /// Determines whether measurement value code is of unit system SI or neutral.
        /// </summary>
        /// <param name="mvCode">The measurement value code.</param>
        /// <returns>
        ///   <c>true</c> if it is of unit system SI or neutral; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUnitSystemSIorNeutral(MVCode mvCode)
        {
            return Enumerable.Contains(MembersUnitSI, mvCode) || IsUnitSystemNeutral(mvCode);
        }

        /// <summary>
        /// Determines whether measurement value code is of unit system US.
        /// </summary>
        /// <param name="mvCode">The measurement value code.</param>
        /// <returns>
        ///   <c>true</c> if it is of unit system US; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUnitSystemUS(MVCode mvCode)
        {
            return Enumerable.Contains(MembersUnitUS, mvCode);
        }

        /// <summary>
        /// Determines whether measurement value code is of unit system US or neutral.
        /// </summary>
        /// <param name="mvCode">The measurement value code.</param>
        /// <returns>
        ///   <c>true</c> if it is of unit system US or neutral; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUnitSystemUSorNeutral(MVCode mvCode)
        {
            return Enumerable.Contains(MembersUnitUS, mvCode) || IsUnitSystemNeutral(mvCode);
        }
    }
}