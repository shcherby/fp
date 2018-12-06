# Functional programming
#### Basic concepts
##### Immutability
* Impossibility to change the state of the object once it was created
* Immutable objects are simpler to test, and use
* Truly immutable objects are always thread-safe
* They help to avoid temporal coupling
* Their usage is side-effect free (no defensive copies)
* Identity mutability problem is avoided
* They always have failure atomicity
* They are much easier to cache
* They prevent NULL references, which are bad

##### Purity
* The property of functions not to have side-effects and any dependence from external state or time
* Function is considered pure when:
* The function always evaluates the same result value given the same argument value(s)
* Evaluation of the result does not cause any side effect   
Application:    
* Simplifies testing
* Simplifies parallel computing
* Allows memoization

##### First-class &  Higher-order functions
##### Recursion
