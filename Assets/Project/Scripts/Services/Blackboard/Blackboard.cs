using System;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard
{
    private Dictionary<int, object> data = new Dictionary<int, object>();
    
    public event Action<BlackboardKey<float>, float> OnFloatChanged;
    public event Action<BlackboardKey<bool>, bool> OnBoolChanged;
    public event Action<BlackboardKey<Vector3>, Vector3> OnVector3Changed;
    
    #region Основные методы
    
    public void SetValue<T>(BlackboardKey<T> key, T value)
    {
        var oldValue = GetValue(key);
        data[key.Id] = value;
        
        // Вызываем события при изменении
        TriggerChangeEvent(key, oldValue, value);
    }
    
    public T GetValue<T>(BlackboardKey<T> key)
    {
        if (data.TryGetValue(key.Id, out object value) && value is T)
        {
            return (T)value;
        }
        return default(T);
    }
    
    public bool TryGetValue<T>(BlackboardKey<T> key, out T value)
    {
        value = default(T);
        if (data.TryGetValue(key.Id, out object objValue) && objValue is T)
        {
            value = (T)objValue;
            return true;
        }
        return false;
    }
    
    public bool HasValue<T>(BlackboardKey<T> key)
    {
        return data.ContainsKey(key.Id);
    }
    
    public void RemoveValue<T>(BlackboardKey<T> key)
    {
        data.Remove(key.Id);
    }
    
    public void Clear()
    {
        data.Clear();
    }
    
    #endregion
    
    #region Удобные методы для частых операций
    
    // Позиции
    public Vector3 GetPosition(BlackboardKey<Vector3> key) => GetValue(key);
    public void SetPosition(BlackboardKey<Vector3> key, Vector3 position) => SetValue(key, position);
    
    // GameObjects
    public GameObject GetGameObject(BlackboardKey<GameObject> key) => GetValue(key);
    public void SetGameObject(BlackboardKey<GameObject> key, GameObject obj) => SetValue(key, obj);
    
    // Списки
    public List<T> GetList<T>(BlackboardKey<List<T>> key) => GetValue(key) ?? new List<T>();
    public void SetList<T>(BlackboardKey<List<T>> key, List<T> list) => SetValue(key, list);
    
    #endregion
    
    #region События
    
    private void TriggerChangeEvent<T>(BlackboardKey<T> key, T oldValue, T newValue)
    {
        if (EqualityComparer<T>.Default.Equals(oldValue, newValue)) return;
        
        switch (newValue)
        {
            case float floatValue when key is BlackboardKey<float> floatKey:
                OnFloatChanged?.Invoke(floatKey, floatValue);
                break;
            case bool boolValue when key is BlackboardKey<bool> boolKey:
                OnBoolChanged?.Invoke(boolKey, boolValue);
                break;
            case Vector3 vectorValue when key is BlackboardKey<Vector3> vectorKey:
                OnVector3Changed?.Invoke(vectorKey, vectorValue);
                break;
        }
    }
    
    #endregion
}

public class LocalBlackboard : Blackboard
{
    public Blackboard GlobalBlackboard { get; private set; }

    public LocalBlackboard(Blackboard blackboard)
    {
        GlobalBlackboard = blackboard;
    }
}