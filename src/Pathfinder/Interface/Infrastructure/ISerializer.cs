namespace Pathfinder.Interface.Infrastructure
{
	internal interface ISerializer<TModel, TSerialized>
	{
		TModel Deserialize(TSerialized pValue);
		TSerialized Serialize(TModel pObject);
	}
}