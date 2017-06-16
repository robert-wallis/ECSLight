using System;
using System.Collections;
using System.Collections.Generic;

namespace ECSLight
{
	public class EntityManager : IEntityManager
	{
		private readonly Dictionary<Entity, Dictionary<Type, IComponent>> _entities;
		private readonly IComponentManager _componentManager;

		public EntityManager(Dictionary<Entity, Dictionary<Type, IComponent>> entities, IComponentManager componentManager)
		{
			_entities = entities;
			_componentManager = componentManager;
		}

		/// <summary>
		/// Make a new entity, or recycle an unused entity.
		/// </summary>
		/// <returns>new empty entity</returns>
		public Entity CreateEntity(string name = "")
		{
			var entity = new Entity(this, _componentManager, name);
			_entities.Add(entity, new Dictionary<Type, IComponent>());
			return entity;
		}

		/// <summary>
		/// Release the entity back to be reused later.
		/// </summary>
		/// <param name="entity">Entity to be released.</param>
		public void ReleaseEntity(Entity entity)
		{
			var types = new List<Type>();
			foreach (var component in entity) {
				types.Add(component.GetType());
			}
			foreach (var type in types) {
				_componentManager.RemoveComponent(entity, type);
			}
			_entities.Remove(entity);
		}

		public IEnumerator<Entity> GetEnumerator()
		{
			return _entities.Keys.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}