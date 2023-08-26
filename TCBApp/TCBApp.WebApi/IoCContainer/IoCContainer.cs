namespace TCBApp.WebApi.IoCContainer;

public class IoCContainer
{
    public  List<Type> dependencies = new List<Type>(); 
        public IoCContainer()
        {
        
        }

        public void Register<T>()
        {
            var storedType = this
                .dependencies
                .FirstOrDefault(x => x == typeof(T));
        
            if (storedType is null)
                this.dependencies.Add(typeof(T));
        }

        public T GetInstance<T>()
        {
            var type = this.dependencies.FirstOrDefault(x => x == typeof(T));
            return (T)ActivateInstance(type);
        }

        public object ActivateInstance(Type type)
        {
            var depType = this.dependencies.FirstOrDefault(x => x == type);
            var defaultConstructor = type.GetConstructors().FirstOrDefault();
       
            if (defaultConstructor is null)
                return Activator.CreateInstance(type);
        
            var parameters = defaultConstructor
                .GetParameters()
                .Select(x => ActivateInstance(x.ParameterType));

            return defaultConstructor.Invoke(parameters.ToArray());
        }
    }