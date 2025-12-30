using UnityEngine;

public class ActionRunner
{
    private UtilityAction current;
    private float timer;
    private float minCommit = 1f;
    public void Run(UtilityAction next)
    {
        if (current == next) return; // Nếu vẫn là hành động cũ thì bỏ qua -> Fix spam log
        if (current !=null && timer < minCommit) return; // Nếu chưa đủ thời gian cam kết -> Bỏ qua
        current?.Stop();
        current = next;
        timer = 0f;
        current?.Start();
    }
    public void Tick(float dt)
    {
        current?.Tick();
        timer += dt;
    }
}
