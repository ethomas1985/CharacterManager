namespace Pathfinder.Interface.Model
{
	internal interface ISerializer<TModel, TSerialized>
	{
		TModel Deserialize(TSerialized pValue);
		TSerialized Serialize(TModel pObject);
	}
}