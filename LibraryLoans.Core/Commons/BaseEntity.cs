namespace LibraryLoans.Core.Commons;

public abstract class BaseEntity<Tid>
{
    public Tid Id { get; set; }

    public abstract void CopyFrom(BaseEntity<Tid> other);
}
