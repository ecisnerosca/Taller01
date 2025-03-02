using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Taller01;

public class Time
{
    private int _hour;
    private int _minute;
    private int _second;
    private int _millisecond;

    public Time()
    {
        _hour = 0;
        _minute = 0;
        _second = 0;
        _millisecond = 0;
    }

    public Time (int hour)
    {
        _hour = ValidateHour(hour);
        _minute = 0;
        _second = 0;
        _millisecond = 0;
    }

    public Time(int hour, int minute)
    {
        _hour = ValidateHour(hour);
        _minute = ValidateMinute(minute);
        _second = 0;
        _millisecond = 0;
    }

    public Time(int hour, int minute, int second)
    {
        _hour = ValidateHour(hour);
        _minute = ValidateMinute(minute);
        _second = ValidateSecond(second);
        _millisecond = 0;
    }

    public Time(int hour, int minute, int second, int millisecond)
    {
        _hour = ValidateHour(hour);
        _minute = ValidateMinute(minute);
        _second = ValidateSecond(second);
        _millisecond = ValidateMillisecond(millisecond);
    }

    public int Hour {
        get => _hour;   
        set => _hour = ValidateHour(value);  
    }

    public int Minute
    {
        get => _minute;
        set => _minute = ValidateMinute(value);
    }

    public int Second
    {
        get => _second;
        set => _second = ValidateSecond(value);
    }

    public int Millisecond
    {
        get => _millisecond;
        set => _millisecond = ValidateMillisecond(value);
    }

    public Time Add(Time otherTime)
    {
        int millisecond = _millisecond + otherTime._millisecond;
        int extraSecond = millisecond / 1000;
        millisecond %= 1000;

        int second = _second + otherTime._second + extraSecond;
        int extraMinute = second / 60;
        second %= 60;

        int minute = _minute + otherTime._minute + extraMinute;
        int extraHour = minute / 60;
        minute %= 60;

        int hour = _hour + otherTime._hour + extraHour;
        bool isNextDay = hour > 23;
        hour %= 24;

        return new Time(hour, minute, second, millisecond);
    }

    public bool IsOtherDay(Time other)
    {
        int totalSeconds = ToSeconds() + other.ToSeconds();
        return totalSeconds >= 86400;
    }

    public int ToMilliseconds()
    {
        if (_hour < 0 || _hour > 23 || _minute < 0 || _minute > 59 ||
            _second < 0 || _second > 59 || _millisecond < 0 || _millisecond > 999)
        {
            return 0;
        }
        return (_hour * 3600000) + (_minute * 60000) + (_second * 1000) + _millisecond;
    }

    public int ToSeconds()
    {
        if (_hour < 0 || _hour > 23 || _minute < 0 || _minute > 59 ||
            _second < 0 || _second > 59 || _millisecond < 0 || _millisecond > 999)
        {
            return 0;
        }
        return (_hour * 3600) + (_minute * 60) + _second;
    }

    public int ToMinutes()
    {
        if (_hour < 0 || _hour > 23 || _minute < 0 || _minute > 59 ||
            _second < 0 || _second > 59 || _millisecond < 0 || _millisecond > 999)
        {
            return 0;
        }
        return (_hour * 60) + _minute;
    }

    public override string ToString()
    {
        int timePeriod = _hour % 12;

        // Medianoche se muestra como 00:00 AM
        if (_hour == 0)
        {
            timePeriod = 0;
        }
        // Mediodía se muestra como 12:00 PM
        else if (_hour == 12)
        {
            timePeriod = 12;
        }

        string amPm = (_hour < 12) ? "AM" : "PM";

        return $"{timePeriod:00}:{_minute:00}:{_second:00}.{_millisecond:00} {amPm}";
    }

    private int ValidateHour(int hour)
    {
        if (hour < 0 || hour > 23)
        {
            throw new ArgumentException($"The hour: {hour} is invalid.");
        }
        return hour;
    }

    private int ValidateMinute(int minute)
    {
        if (minute < 0 || minute > 59)
        {
            throw new ArgumentException($"The minute: {minute} is invalid.");
        }
        return minute;

    }
    private int ValidateSecond(int second)
    {
        if (second < 0 || second > 59)
        {
            throw new ArgumentException($"The second: {second} is invalid.");
        }
        return second;
    }

    private int ValidateMillisecond(int millisecond)
    {
        if (millisecond < 0 || millisecond > 999)
        {
            throw new ArgumentException($"The millisecond: {millisecond} is invalid.");
        }
        return millisecond;
    }
}
