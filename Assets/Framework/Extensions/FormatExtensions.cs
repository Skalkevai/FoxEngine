
public static class FormatExtensions
{
    public static string AddSpaceBeforeCapital(this string _text)
    {
        string newString = "";
        
        newString += _text[0];
        for (int i = 1; i < _text.Length; i++)
        {
            if (char.IsUpper(_text[i]) && !_text[i-1].Equals(' ') && !_text[i-1].Equals('_'))
                newString += " ";
            newString += _text[i];
        }
        
        return newString;
    }
    
    public static string FormatTime(this float _time,bool _useMillisecond = false)
    {
        int minutes = (int)_time / 60;
        int seconds = (int)_time - 60 * minutes;
        int milliseconds = (int)(1000 * (_time - minutes * 60 - seconds)) / 10;
        if(_useMillisecond)
            return $"{minutes:00}:{seconds:00}:{milliseconds:00}";
        else
            return $"{minutes:00}:{seconds:00}";
    }
}
