namespace Pathfinder.Interface
{
	internal interface ISerializer<TModel, TSerialized>
	{
		TModel Deserialize(TSerialized pValue);
		TSerialized Serialize(TModel pObject);
	}
}