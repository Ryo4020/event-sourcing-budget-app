namespace BudgetAppProject.DomainModel.Utils;

public static class EnumHelper
{
    public static TEnum ParseEnumFromInt<TEnum>(int value) where TEnum : Enum
    {
        if (!Enum.IsDefined(typeof(TEnum), value))
        {
            throw new ArgumentException($"Value {value} is not defined in enum {typeof(TEnum).Name}");
        }

        return (TEnum)Enum.ToObject(typeof(TEnum), value);
    }

    public static int GetValueEnum(Enum enumValue)
    {
        return Convert.ToInt16(enumValue);
    }
}