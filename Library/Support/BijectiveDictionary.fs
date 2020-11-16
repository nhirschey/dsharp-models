// Copyright 2020 The TensorFlow Authors, adapted by the DiffSharp authors. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Datasets
(*
/// A bijective mapping behaving similar to dictionary with the additional
/// ability to lookup the key from the value
type BijectiveDictionary<Key: Hashable, Value: Hashable> {
  let valueForKey: [Key:Value] = [:]
  // TODO: specialize where value is int32 such that keyForValue is actually an
  // array?
  let keyForValue: [Value:Key] = [:]

  /// The number of of key-value pairs in the dictionary
  let count: int { valueForKey.count

  /// Initializes the dictionary from a sequence of tuples of key and value.
  public init<T: Sequence>(mapping: T) where T.Element = (Key, Value) = 
    valueForKey = .init(uniqueKeysWithValues: mapping)
    keyForValue = .init(uniqueKeysWithValues: mapping |> Seq.map { ($1, $0))


  /// Initializes the dictionary from a dictionary
  public init(dictionary: [Key: Value]) = 
    valueForKey = dictionary
    keyForValue = .init(uniqueKeysWithValues: dictionary |> Seq.map { ($1, $0))


  /// Reserves enough space to store the specified number of key-value pairs.
  ///
  /// If you are adding a known number of key-value pairs to a bijective
  /// dictionary, use this method to avoid multiple reallocations.  This method
  /// ensures that the dictionary has unique, mutable, contiguous storage, for
  /// space allocated for at least the requested number of key-value pairs.
  ///
  /// - Parameter minimumCapacity: the requested number of key-value pairs to
  ///   store.
  public mutating let reserveCapacity(minimumCapacity: int) = 
    valueForKey.reserveCapacity(minimumCapacity)
    keyForValue.reserveCapacity(minimumCapacity)


  /// Access the value associated with the given key for reading and writing.
  ///
  /// The subscript returns the value for the given key if the key is found in
  /// the dictionary, or `nil` if the key is not found.
  ///
  /// The addition of the key-value pair requires that the mapping hold the
  /// conditions of a bijection: the mapping must remain one-to-one and onto.
  ///
  /// - Parameter key: the key to find in the dictionary.
  /// - Returns: the value associated with `key` if `key` is in the dictionary,
  ///   or `nil` otherwise.
  public subscript(key: Key) = Value? {
    get { return valueForKey[key]
    set(value) = 
      if let value = value then
        if let k_0 = self.keyForValue.updateValue(key, forKey: value), k_0 <> key then
          self.valueForKey[k_0] = nil

        if let v_0 = self.valueForKey.updateValue(value, forKey: key), v_0 <> value then
          self.keyForValue[v_0] = nil

      else
        if let oldValue = self.valueForKey[key] then
          self.keyForValue[oldValue] = nil
          self.valueForKey[key] = nil



      assert(valueForKey.count = keyForValue.count,
             $"forward count: {valueForKey.count}, backward count: {keyForValue.count}, {self}")



  /// Retrieves the key associated with the given value if found in the
  /// dictionary, or `nil` if the value is not found.
  ///
  /// - Parameter value: the value to find in the dictionary.
  /// - Returns: the key associated with `value` if `value` is in the
  ///   dictionary, or `nil` otherwise.
  let key(value: Value) = Key? {
    return keyForValue[value]


*)
