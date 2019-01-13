### Operations
* List ranges
```
['a','b'] @ ['c';'d'] = ['a','b','c';'d']
```
* List concatenation
```
['a','b'] @ ['c';'d'] = ['a','b','c';'d']
```

* Records
```
type Customer = 
    { First: string
      Last: string
      SSN: uint32
      AccountNumber: uint32; }
```

* Discriminated unions provide support for values that can be one of a number of named cases
```
type Shape =
    | Rectangle of width : float * length : float
    | Circle of radius : float
    | Prism of width : float * float * height : float
```

* A generic record, with the type parameter in angle brackets.
```
type GR<'a> =
    {
        Field1: 'a;
        Field2: 'a;
    }
```
