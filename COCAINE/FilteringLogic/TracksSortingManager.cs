using COCAINE.Models.DomainModels;

namespace COCAINE.FilteringLogic
{
    public class TracksSortingManager
    {
        private readonly IEnumerable<Track> _originalCollection;

        public TracksSortingManager(IEnumerable<Track> originalCollection)
        {
            _originalCollection = originalCollection;
        }

        public IEnumerable<Track> ApplySort(string filter)
        {
            if (filter == null)
                return _originalCollection;

            var filterStatements = filter.Split(',').ToList();
            filterStatements.ForEach(statement => statement.Trim());

            return ApplySortRecourisve(_originalCollection, filterStatements, true);
        }

        private IEnumerable<Track> ApplySortRecourisve(IEnumerable<Track> collection, List<string> filterStatements, bool isFirst)
        {
            if (filterStatements.Count == 0)
                return collection;

            if (isFirst)
                collection = ApplySortingPartition(collection, filterStatements.First());
            else
                collection = ApplyThenSortingPartition(collection as IOrderedEnumerable<Track>, filterStatements.First());

            filterStatements.RemoveAt(0);

            return ApplySortRecourisve(collection, filterStatements, false);
        }

        private IOrderedEnumerable<Track> ApplySortingPartition(IEnumerable<Track> collection, string filterStatement)
        {
            switch (filterStatement.ToLower())
            {
                case "sort by id":              return collection.OrderBy(t => t.Id);
                case "sort by id desc":         return collection.OrderByDescending(t => t.Id);
                case "sort by trackname":       return collection.OrderBy(t => t.TrackName);
                case "sort by trackname desc":  return collection.OrderByDescending(t => t.TrackName);
            }

            return collection.OrderBy(t => t.Id);
        }

        private IOrderedEnumerable<Track> ApplyThenSortingPartition(IOrderedEnumerable<Track> collection, string filterStatement)
        {
            switch (filterStatement.ToLower())
            {
                case "id":                      return collection.ThenBy(t => t.Id);
                case "id desc":                 return collection.ThenByDescending(t => t.Id);
                case "trackname":               return collection.ThenBy(t => t.TrackName);
                case "trackname desc":          return collection.ThenByDescending(t => t.TrackName);
            }

            return collection;
        }

        
    }
}
