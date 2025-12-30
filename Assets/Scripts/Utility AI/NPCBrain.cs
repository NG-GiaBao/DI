using System;
using System.Collections.Generic;
using UnityEngine;

public struct ContextData
{
    public KeyData Key;
    public ConditionData Condition;
    public float KeyValue;
    public float ConditionValue;
}
public class NPCBrain : MonoBehaviour
{
    [SerializeField] private UtilityAIController ai;
    [SerializeField] private ActionRunner runner;
    [SerializeField] private AIContext context;
    [SerializeField] private ContextUpdater contextUpdater;
    [SerializeField] private ContextSO contextSO;

    private void Awake()
    {
        BuildAI();
    }

    private void BuildAI()
    {
        context = new AIContext();
        // Copy dữ liệu từ SO vào bộ nhớ chạy thật
        ContextData hungerData = new()
        {
            Key = contextSO.GetKey(KeyData.Hunger),
            KeyValue = contextSO.GetKeyValue(KeyData.Hunger),
            Condition = contextSO.GetCondition(ConditionData.IsEating),
            ConditionValue = contextSO.GetConditionValue(ConditionData.IsEating),
        };
        ContextData fearData = new()
        {
            Key = contextSO.GetKey(KeyData.Fear),
            KeyValue = contextSO.GetKeyValue(KeyData.Fear),
            Condition = contextSO.GetCondition(ConditionData.IsRunning),
            ConditionValue=contextSO.GetConditionValue(ConditionData.IsRunning),
        };

        context.AddKey(hungerData);
        context.AddCondition(hungerData);
        context.AddKey(fearData);
        context.AddCondition(fearData);

        contextUpdater = new ContextUpdater();

        // Rule 1: Tự nhiên thì luôn đói dần (Drift)
        contextUpdater.AddRule(new NaturalDriftRule(hungerData));

        // Rule 3: Nếu đang Ăn (IsEating > 0) -> Đói giảm cực nhanh
        contextUpdater.AddRule(new ConditionalModifyRule(hungerData));

        // Rule 4: Nếu đang Chạy (IsRunning > 0) -> Sợ giảm cực nhanh
        contextUpdater.AddRule(new ConditionalModifyRule(fearData));

        // Rule 2: Tự nhiên thì nỗi sợ cũng dâng lên (Paranoia)
        contextUpdater.AddRule(new NaturalDriftRule(fearData));

        ai = new UtilityAIController(new List<UtilityAction>
        {
            new EatAction(context,hungerData,fearData),
            new RunAction(context,fearData)
        });

        runner = new ActionRunner();
    }

    private void Update()
    {
        // 1. Cập nhật các chỉ số (Rules chạy)
        contextUpdater.Tick(context, Time.deltaTime);
        // 2. Bộ não suy nghĩ & chọn Action tốt nhất
        var action = ai.Choose(context);
        // 3. Thực thi Action đó
        runner.Run(action);
        runner.Tick(Time.deltaTime);
    }
}
