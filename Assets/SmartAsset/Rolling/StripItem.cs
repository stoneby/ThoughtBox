
public class StripItem
{
    public int Id;
    public string Name;

    public override bool Equals(object o)
    {
        if (ReferenceEquals(null, o)) return false;
        if (ReferenceEquals(this, o)) return true;
        return o.GetType() == GetType() && Equals((StripItem) o);
    }

    protected bool Equals(StripItem other)
    {
        return base.Equals(other) && Id == other.Id && string.Equals(Name, other.Name);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ Id;
            hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            return hashCode;
        }
    }

}
