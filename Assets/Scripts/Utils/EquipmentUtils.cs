public static class EquipmentUtils
{
    public static string Source_Path = "Assets/Data Objects/Game Items/";

    public static string GetFullPath(string ID)
    {
        return Source_Path + ID + ".asset"; 
    }

    public static string GetItemAddress(string ID)
    {
        string cutString = ID.Replace(Source_Path, "").Replace(".asset", "");
        cutString = cutString.Split('/')[1];
        return cutString;
    }

    
    
}
