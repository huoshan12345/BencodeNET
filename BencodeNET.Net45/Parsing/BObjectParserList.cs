﻿using System;
using System.Collections;
using System.Collections.Generic;
using BencodeNET.Objects;

namespace BencodeNET.Parsing
{
    public class BObjectParserList : IEnumerable<KeyValuePair<Type, IBObjectParser>>
    {
        private IDictionary<Type, IBObjectParser> Parsers { get; } = new Dictionary<Type, IBObjectParser>();

        public void Add(Type type, IBObjectParser parser)
        {
            Parsers.Add(type, parser);
        }

        public void Add<T>(IBObjectParser<T> parser) where T : IBObject
        {
            AddOrReplace(typeof(T), parser);
        }

        public void AddOrReplace(Type type, IBObjectParser parser)
        {
            if (Parsers.ContainsKey(type))
                Parsers.Remove(type);
            Parsers.Add(type, parser);
        }

        public void AddOrReplace<T>(IBObjectParser<T> parser) where T : IBObject
        {
            AddOrReplace(typeof(T), parser);
        }

        public IBObjectParser Get(Type type)
        {
            return Parsers.GetValueOrDefault(type);
        }

        public IBObjectParser this[Type type]
        {
            get { return Get(type); }
            set { AddOrReplace(type, value); }
        }

        public IBObjectParser<T> Get<T>() where T : IBObject
        {
            return Get(typeof(T)) as IBObjectParser<T>;
        }

        public bool Remove(Type type) => Parsers.Remove(type);

        public bool Remove<T>() => Remove(typeof (T));

        public void Clear() => Parsers.Clear();

        public IEnumerator<KeyValuePair<Type, IBObjectParser>> GetEnumerator()
        {
            return Parsers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
