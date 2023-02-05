namespace Space_Shooter.Storage
{
    class CollisionData
    {
        public ObjectOnCanvas Object1;
        public ObjectOnCanvas Object2;

    public CollisionData(ObjectOnCanvas object1, ObjectOnCanvas object2)
        {
            Object1 = object1;
            Object2 = object2;
        }
    }
}
