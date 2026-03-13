using System.Reflection;

namespace NYC311Dashboard.Mapping
{
    public static class ModelMapper
    {
        public static TTarget Map<TSource, TTarget>(TSource source, Action<TSource, TTarget>? overrides = null)
            where TTarget : new()
        {
            var target = new TTarget();
            var sourceProps = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProps = typeof(TTarget).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(p => p.Name);

            foreach (var sourceProp in sourceProps)
            {
                if (targetProps.TryGetValue(sourceProp.Name, out var targetProp)
                    && targetProp.CanWrite
                    && targetProp.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
                {
                    targetProp.SetValue(target, sourceProp.GetValue(source));
                }
            }

            overrides?.Invoke(source, target);
            return target;
        }
    }
}
