using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

public enum GameSpeed
{
    Pause,
    VerySlow,
    Slow,
    Medium,
    Fast,
}

public class GameTimeKeeper : Singleton<GameTimeKeeper>
{
    public GameTimeKeeperConfig _config;
    volatile bool isPaused = false, isStopped = true, isRunning = false;
    Stack<GameTimeStruct> _tickpool;
    public GameTimeStruct _current, _previous;
    object _lock = new object();
    GameSpeed _speed;

    public GameTimeEventTick TickEvent;

    private void Awake()
    {
        TickEvent = new GameTimeEventTick();
        _config = new GameTimeKeeperConfig();
        _tickpool = new Stack<GameTimeStruct>(1000);
        GameTimeKeeper refgtk = this;
        _current = new GameTimeStruct(ref refgtk);
        _previous = new GameTimeStruct(ref refgtk);
        _lock = new object();
        var s = this;
        for (int i = 0; i < 1000; i++)
        {
            _tickpool.Push(new GameTimeStruct(ref s));
        }
    }

    private void Update()
    {
        //only if we're in single player do we update the game locally
        //if (!NetworkSingleton.Instance.IsMultiplay && !isStopped)
        //{
        //    //now we update the partialTick value
        //    switch (_speed)
        //    {
        //        case GameSpeed.Paused:
        //            //do nothing
        //            break;
        //        case GameSpeed.VerySlow:
        //            lock(_lock) { _current.Increment(Time.deltaTime * _config._veryslow); }
        //            //lock (_lock) { _current.partialTick += (Time.deltaTime * _config._veryslow); }
        //            break;
        //        case GameSpeed.Slow:
        //            lock (_lock) { _current.Increment(Time.deltaTime * _config._slow); }
        //            //lock (_lock) { _current.partialTick += (Time.deltaTime * _config._slow); }
        //            break;
        //        case GameSpeed.Medium:
        //            lock (_lock) { _current.Increment(Time.deltaTime * _config._medium); }
        //            //lock (_lock) { _current.partialTick += (Time.deltaTime * _config._medium); }
        //            break;
        //        case GameSpeed.Fast:
        //            lock (_lock) { _current.Increment(Time.deltaTime * _config._fast); }
        //            //lock (_lock) { _current.partialTick += (Time.deltaTime * _config._fast); }
        //            break;
        //        default:
        //            //somehow we have an enum value given to us that is not explicitly handled.
        //            //most likely someone made a sloppy edit or its the result of disassembled/recompiled unity code
        //            UnityLogSystemSingleton.Instance.HandleLog(
        //                "GameTimeKeeper.Update() has encountered a GameSpeed enumeration value not supported.",
        //                "Value= " + _speed.ToString(),
        //                LogType.Error
        //                );
        //            break;
        //    }

        //    //if time has advanced by at least 1 tick, notify all handlers
        //    if (!_current.Equals(_previous))
        //    {
        //        _previous.Assign(ref _current);
        //        TickEvent.Invoke(_current);
        //    }
        //}
        //if it .IsMultiplay, then network events will handle updating the current clock time
        //so nothing else needs to be handled here.
    }

    //public void SavedGameInsert(ref SavedGameFileFormat savedGame)
    //{
    //    savedGame.SetTime(ref _current);
    //}

    //public void SavedGameLoad(ref SavedGameFileFormat savedGame)
    //{
    //    _current = GameTimeStruct.FromString(savedGame.DataSet.Tables["Meta"].Rows[0]["GameTimeStruct"].ToString());
    //}

    /// <summary>
    /// Starts the GameTimeKeeper invoking the TickEvent callback.
    /// </summary>
    public void TimerStart()
    {
        if (!isRunning && isStopped)
        {
            isPaused = false; isStopped = false; isRunning = true;
            //Debug.Log("Timer Started");
        }
    }

    /// <summary>
    /// Starts the GameTimeKeeper invoking the TickEvent callback using the 'config' parameter
    /// given as settings for timer behavior.
    /// </summary>
    /// <param name="config"></param>
    public void TimerStart(GameTimeKeeperConfig config)
    {
        _config = config;
        if (!isRunning && isStopped)
        {
            isPaused = false; isStopped = false; isRunning = true;
        }
    }

    /// <summary>
    /// Stops the GameTimeKeeper from invoking the TickEvent callback.  Calling this method resets
    /// the ElapsedSessionTicks and ElapsedSessionRealSeconds properties.
    /// </summary>
    public void TimerStop()
    {
        if (isRunning) isPaused = false; isStopped = true; isRunning = false;
    }

    /// <summary>
    /// Stops the GameTimeKeeper from invoking the TickEvent callback.  This is meant to be a 
    /// transitive state, and by calling TimerResume() will allow normal progression of TickEvent
    /// callbacks.  Calling TimerStart() after TimerPause() will be ignored.
    /// </summary>
    public void TimerPause()
    {
        if (isRunning) isPaused = true; isStopped = false; isRunning = true;
    }

    /// <summary>
    /// Starts the GameTimeKeeper invoking the TickEvent callback *after* a TimerPause() call
    /// was made.
    /// </summary>
    public void TimerResume()
    {
        if (!isRunning && isPaused) { isPaused = false; isStopped = false; isRunning = true; }
    }

    public void ChangeSpeed(GameSpeed speed)
    {
        /*      so we use "number line" reasoning to reduce the logic involved to mathematical operations
                --Pause-----VerySlow-----Slow-----Medium-----Fast
                Veryslow is 45seconds for 1 game-day, each day is 3 ticks
                so 1 Tick occurs every completed 45/3 seconds (15)
                When a speed change occurs, we see how far along the current Time value is for a Tick
                example: a speed change from VerySlow to Medium (10s per game-day) occurs at 20 seconds
                This means the new current time is 1 Tick + (5/45th Tick) at Medium speed.
                With the next Tick occuring at the expected interval of NextTick(MediumSpeed) - currentTime
                */
        _speed = speed;
    }

    public void Release(GameTimeStruct tick)
    {
        //_tickpool.Push(tick);
    }

    public GameTimeStruct Now()
    {
        if (_tickpool.Count > 0)
        {
            var z = _tickpool.Pop();
            z.Assign(ref _current);
            return z;
        }
        else
        {
            GameTimeStruct zz = new GameTimeStruct();
            zz.Assign(ref _current);
            return zz;
        }

    }

}

public class GameTimeEventTick : UnityEngine.Events.UnityEvent<GameTimeStruct>
{

}

#if ODIN_INSPECTOR
[ShowInInspector]
#endif
public class GameTimeStruct
{
    private GameTimeKeeper _parent;

    internal float partialTick;

    public float Tick;
    public int TickInt { get { return (int)Tick; } }

    //current "day" of the game
    public int GameDay
    {
        get { return (TickInt / GameTimeKeeper.Instance._config.TicksPerDay); }
    }
    public int GameDayTick
    {
        get { return (TickInt % GameTimeKeeper.Instance._config.TicksPerDay); }
    }

    //Total number of Ticks that have elapsed since the game started
    public float ElapsedTicks;
    public int ElapsedTicksInt { get { return (int)ElapsedTicks; } }

    //Total number of real seconds that have elapsed since the game started
    public float ElapsedRealSeconds;
    public int ElapsedRealSecondsInt { get { return (int)ElapsedRealSeconds; } }

    //Total number of Ticks that have elapsed since the current game session started
    public float ElapsedSessionTicks;
    public int ElapsedSessionTicksInt { get { return (int)ElapsedSessionTicks; } }

    //Total number of real seconds that have elapsed since the current game session started
    public float ElapsedSessionRealSeconds;
    public int ElapsedSessionRealSecondsInt { get { return (int)ElapsedSessionRealSeconds; } }

    public void Increment(float value)
    {
        partialTick += value;
        if (partialTick >= 1f)
        {   //now we increment the Tick value
            Tick += partialTick;
            partialTick = 0f;
            //Debug.Log("Tick incremented, PT=0");
        }
    }

    public bool Equals(GameTimeStruct obj)
    {
        if (this.TickInt == obj.TickInt) return true;

        return false;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override string ToString()
    {
        System.Text.StringBuilder str = new System.Text.StringBuilder();
        //str.Append("PT=" + partialTick.ToString("N4") + "//");
        str.Append("PT=" + partialTick.ToString() + "//");
        str.Append("T=" + TickInt.ToString() + "//");
        str.Append("GD=" + GameDay.ToString() + "//");
        str.Append("GDT=" + GameDayTick.ToString() + "//");
        str.Append("ERS=" + ElapsedRealSeconds.ToString() + "//");
        str.Append("EST=" + ElapsedSessionTicksInt.ToString() + "//");
        str.Append("ET=" + ElapsedTicks.ToString());

        return str.ToString();
    }
    public static GameTimeStruct FromString(string value)
    {
        GameTimeStruct result = new GameTimeStruct();
        var lst = value.Split("//", System.StringSplitOptions.RemoveEmptyEntries);
        //Debug.Log(lst[0] + lst[1] + lst[2] + lst[3] + lst[4] + lst[5]);
        result.partialTick = float.Parse(lst[0].Substring(3, lst[0].Length - 3));
        result.Tick = float.Parse(lst[1].Substring(2, lst[1].Length - 2));
        //GameDay and GameDayTick are calculated values, so we skip them
        result.ElapsedRealSeconds = float.Parse(lst[4].Substring(4, lst[4].Length - 4));
        result.ElapsedSessionTicks = int.Parse(lst[5].Substring(4, lst[5].Length - 4));
        result.ElapsedTicks = float.Parse(lst[6].Substring(3, lst[6].Length - 3));

        return result;
    }

    public void Assign(ref GameTimeStruct val)
    {
        partialTick = val.partialTick;
        Tick = val.Tick;
    }

#region Operator overloads
    public static bool operator >(GameTimeStruct lhs, GameTimeStruct rhs)
    {
        if (lhs.GameDay > rhs.GameDay) return true;
        else if (lhs.GameDay == rhs.GameDay)
        {
            if (lhs.TickInt > rhs.TickInt) return true;

            return false;
        }
        else if (lhs.GameDay < rhs.GameDay)
        {
            return false;
        }

        return false;
    }
    public static bool operator >=(GameTimeStruct lhs, GameTimeStruct rhs)
    {
        if (lhs.GameDay > rhs.GameDay) return true;
        else if (lhs.GameDay == rhs.GameDay)
        {
            if (lhs.TickInt > rhs.TickInt) return true;

            return false;
        }
        else if (lhs.GameDay < rhs.GameDay)
        {
            return false;
        }

        return false;
    }
    public static bool operator <(GameTimeStruct lhs, GameTimeStruct rhs)
    {
        if (lhs.GameDay < rhs.GameDay) return true;
        else if (lhs.GameDay == rhs.GameDay)
        {
            if (lhs.TickInt < rhs.TickInt) return true;

            return false;
        }
        else if (lhs.GameDay > rhs.GameDay)
        {
            return false;
        }

        return false;
    }
    public static bool operator <=(GameTimeStruct lhs, GameTimeStruct rhs)
    {
        if (lhs.GameDay < rhs.GameDay) return true;
        else if (lhs.GameDay == rhs.GameDay)
        {
            if (lhs.TickInt < rhs.TickInt) return true;

            return false;
        }
        else if (lhs.GameDay > rhs.GameDay)
        {
            return false;
        }

        return false;
    }
    public static bool operator ==(GameTimeStruct lhs, GameTimeStruct rhs)
    {
        if (lhs.GameDay == rhs.GameDay && lhs.TickInt == rhs.TickInt) return true;

        return false;
    }
    public static bool operator !=(GameTimeStruct lhs, GameTimeStruct rhs)
    {
        if (lhs == rhs) return true;
        return false;
    }
    public static GameTimeStruct operator +(GameTimeStruct lhs, GameTimeStruct rhs)
    {
        lhs.Tick += rhs.Tick;
        return lhs;
    }
    public static GameTimeStruct operator -(GameTimeStruct lhs, GameTimeStruct rhs)
    {
        lhs.Tick -= rhs.Tick;
        return lhs;
    }
#endregion

    internal GameTimeStruct(ref GameTimeKeeper pool)
    {
        if (pool == null) throw new System.ArgumentNullException("#1 GameTimeKeeper can not be null.");

        Tick = 1;
        ElapsedTicks = 1;
        ElapsedRealSeconds = 1;
        ElapsedSessionTicks = 1;
        ElapsedSessionRealSeconds = 1;

        _parent = pool;
    }
    public GameTimeStruct()
    {

    }

    ~GameTimeStruct()
    {
        Release();
    }

    public void Release()
    {
        _parent.Release(this);
    }
}

public class GameTimeKeeperConfig
{
    public int TicksPerDay = 3;
    public int _veryslow = 45, _slow = 25, _medium = 10, _fast = 8;

    //public int VerySlow
    //{
    //    get { return _veryslow; }
    //    set
    //    {
    //        if (value >= 0 && value <= 300 && value < _slow) _veryslow = value;
    //    }
    //}
    //public int Slow
    //{
    //    get { return _slow; }
    //    set
    //    {
    //        if (value >= 0 && value <= 300 && value < _medium) _slow = value;
    //    }
    //}
    //public int Medium
    //{
    //    get { return _medium; }
    //    set
    //    {
    //        if (value >= 0 && value <= 300 && value < _fast) _medium = value;
    //    }
    //}
    //public int Fast
    //{
    //    get { return _fast; }
    //    set
    //    {
    //        if (value >= 0 && value <= 300 && value >= _fast) _fast = value;
    //    }
    //}
}

public class GameTimeReference
{
    public int Day, Tick;

    public override string ToString()
    {
        return Day.ToString() + " " + Tick.ToString();
    }

    public bool LessThan(GameTimeReference b)
    {
        if (this.Day < b.Day) return true;
        else if (this.Day == b.Day)
        {
            if (this.Tick < b.Tick) return true;
            return false;
        }
        else { return false; }
    }

    public bool LessThanEqual(GameTimeReference b)
    {
        if (this.Day < b.Day)
        {
            if (this.Tick > b.Tick) { return false; }
            else if (this.Tick == b.Tick) { return true; }
            else if (this.Tick < b.Tick) { return true; }

            return false; //satisfy the C# compiler
        }
        else if (this.Day == b.Day)
        {   //evaluate if the Tick value makes 'this' LessThanEqual
            if (this.Tick > b.Tick) { return false; }
            else if (this.Tick == b.Tick) { return true; }
            else if (this.Tick < b.Tick) { return true; }

            return false; //satisfy the C# compiler
        }
        else
        {   //this.Day is > b.Day, thus catagorically false for LessThanEqual <=
            return false;
        }
    }

    public bool GreaterThan(GameTimeReference b)
    {
        if (this.Day > b.Day)
        {
            return true;
        }
        else if (this.Day == b.Day)
        {
            if (this.Tick > b.Tick) { return true; }
            else if (this.Tick == b.Tick) { return false; }
            else if (this.Tick < b.Tick) { return false; }

            return false;
        }
        else if (this.Day < b.Day) { return false; }

        return false;
    }

    public bool GreaterThanEqual(GameTimeReference b)
    {
        if (this.Day > b.Day)
        {
            return true;
        }
        else if (this.Day == b.Day)
        {
            if (this.Tick > b.Tick) { return true; }
            else if (this.Tick == b.Tick) { return true; }
            else if (this.Tick < b.Tick) { return false; }
        }
        else if (this.Day < b.Day) { return false; }

        return false;
    }

    public bool EqualTo(GameTimeReference b)
    {
        if (this.Day > b.Day)
        {
            return false;
        }
        else if (this.Day == b.Day)
        {
            if (this.Tick > b.Tick) { return false; }
            else if (this.Tick == b.Tick) { return true; }
            else if (this.Tick < b.Tick) { return false; }

            return false;
        }
        else if (this.Day < b.Day) { return false; }

        return false;
    }
}