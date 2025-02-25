﻿using Robust.Shared.IoC;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.Manager;
using Robust.Shared.Serialization.Markdown;
using Robust.Shared.Serialization.Markdown.Validation;
using Robust.Shared.Serialization.Markdown.Value;
using Robust.Shared.Serialization.TypeSerializers.Interfaces;

namespace Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype
{
    public sealed class AbstractPrototypeIdSerializer<TPrototype> : PrototypeIdSerializer<TPrototype>
        where TPrototype : class, IPrototype
    {
        public override ValidationNode Validate(ISerializationManager serializationManager, ValueDataNode node,
            IDependencyCollection dependencies, ISerializationContext? context = null)
        {
            return dependencies.Resolve<IPrototypeManager>().HasMapping<TPrototype>(node.Value)
                ? new ValidatedValueNode(node)
                : new ErrorNode(node, $"PrototypeID {node.Value} for type {typeof(TPrototype)} not found");
        }
    }

    [Virtual]
    public class PrototypeIdSerializer<TPrototype> : ITypeSerializer<string, ValueDataNode> where TPrototype : class, IPrototype
    {
        public virtual ValidationNode Validate(ISerializationManager serializationManager, ValueDataNode node,
            IDependencyCollection dependencies, ISerializationContext? context = null)
        {
            return dependencies.Resolve<IPrototypeManager>().HasIndex<TPrototype>(node.Value)
                ? new ValidatedValueNode(node)
                : new ErrorNode(node, $"PrototypeID {node.Value} for type {typeof(TPrototype)} not found");
        }

        public string Read(ISerializationManager serializationManager, ValueDataNode node,
            IDependencyCollection dependencies, bool skipHook, ISerializationContext? context = null, string? value = default)
        {
            return node.Value;
        }

        public DataNode Write(ISerializationManager serializationManager, string value, bool alwaysWrite = false,
            ISerializationContext? context = null)
        {
            return new ValueDataNode(value);
        }

        public string Copy(ISerializationManager serializationManager, string source, string target, bool skipHook,
            ISerializationContext? context = null)
        {
            return source;
        }
    }
}
