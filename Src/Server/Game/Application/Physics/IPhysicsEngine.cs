public interface IPhysicsEngine<T>
{
    public float MoveHorizontal(T entity, IMapGame map, float deltaTime);
}
