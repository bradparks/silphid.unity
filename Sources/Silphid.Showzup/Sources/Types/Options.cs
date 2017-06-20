﻿using System.Collections.Generic;

namespace Silphid.Showzup
{
    public class Options
    {
        /// <summary>
        /// Whether this is a forward (push) or backward (pop) navigation.
        /// </summary>
        public Direction Direction { get; set; }
        
        /// <summary>
        /// With NavigationControl, determines how this item will affect navigation history.
        /// </summary>
        public PushMode PushMode { get; set; }
        
        /// <summary>
        /// Extra variants to combine with presenter's variants in order to determine proper ViewModel/View/Prefab.
        /// </summary>
        public VariantSet Variants { get; set; } = VariantSet.Empty;
        
        /// <summary>
        /// Allows routing presenters to forward presentation request to a presenter supporting given variant.
        /// </summary>
        public IVariant Target { get; set; }
        
        /// <summary>
        /// With TransitionControl, what transition to use. If none specified, falls back to presenter's default
        /// transition or to InstantTransition.
        /// </summary>
        public ITransition Transition { get; set; }
        
        /// <summary>
        /// With TransitionControl, the duration of transition to use. If none specified, falls back to presenter's
        /// default transition's duration.
        /// </summary>
        public float? TransitionDuration { get; set; }

        /// <summary>
        /// Optional instances that will be bound to their own type and automatically injected into ViewModel and/or
        /// View as extra bindings.
        /// </summary>
        public IEnumerable<object> Parameters { get; set; }

        public override string ToString() =>
            $"{nameof(Direction)}: {Direction}, {nameof(PushMode)}: {PushMode}, {nameof(Variants)}: {Variants}, {nameof(Transition)}: {Transition}, {nameof(TransitionDuration)}: {TransitionDuration}";

        public static Options Clone(Options other) =>
            new Options
            {
                Direction = other?.Direction ?? Direction.Default,
                PushMode = other?.PushMode ?? PushMode.Default,
                Variants = other?.Variants ?? VariantSet.Empty,
                Transition = other?.Transition,
                TransitionDuration = other?.TransitionDuration
            };

        public static Options CloneWithExtraVariants(Options other, VariantSet extraVariants)
        {
            var clone = Clone(other);
            clone.Variants = other?.Variants.UnionWith(extraVariants) ?? extraVariants;
            return clone;
        }
    }
}