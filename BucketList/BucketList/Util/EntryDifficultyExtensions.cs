namespace BucketList.Util
{
    using DAL;
    using System.ComponentModel;
    using System.Reflection;

    public static class EntryDifficultyExtensions
    {
        public static string ToDisplayString(this EntryDifficulty difficulty)
        {
            MemberInfo[] memberInfo = typeof(EntryDifficulty).GetMember(difficulty.ToString());
            if (memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return difficulty.ToString();
        }
    }
}