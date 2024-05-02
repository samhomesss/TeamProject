using System.Collections.Generic;

public class ProfileDataModel
{
    private string _nickname;
    private List<string> _achievements = new List<string>();
    public string nickname { get => _nickname; set => _nickname = value; }
    public List<string> achievements { get => _achievements; set => _achievements = value; }
}