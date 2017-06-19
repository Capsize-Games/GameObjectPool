# GameObjectPool

Easily create and access pools of GameObjects to save on money.

`GameObjectPool.Pool` extends `System.Collections.Generic.Queue` and takes
instructions in the form of serialized `PoolSettings` objects.

## PoolSettings

- `Name` - the name of the pool
- `Prefab` - the prefab item that will fill the pool
- `Starting Item Count` - the number of items to initially create
- `Max Item Count` - the number of items to add to the pool before triggering an error message
- `Parent` - transform to parent pooled objects to
- `Allow Unrestricted Growth` - prevent error message from triggering

## How to use

See **INSTALLATION.md** on how to import into a project.

See **Example > Scenes > Test** for sample usage involving enemy and player bullet
pools.

1. Assign `PoolManager` to a game object in your main scene.
2. On that script set Pool to a number greater than zero -- these are the number
of pools you are creating.
3. A list of properties under the dropdown "Element N" will show up, fill out those properties
4. At run time the queue is created and `GameObjectPool.PooledItem` is attached to each instantiated prefab (which was assigned in step 3)
5. To get an object from the pool `PoolManager.Get("PoolName")`
6. To return an object to the pool, simply deactivate from whichever script is using it with `pooledObject.SetActive(false)` or see Bullet.cs for self deactivating example - the item will be added back to the queue.

## Pool is full and leaking over

If the pool leaks over (goes over the max item count that you set) then a logged
error will occur.

Checking "Allow Unrestricted Grow" in the pool settings will prevent this error
message and allow the pool to grow.

**The example herein has Max Item Count set too low in order to
show what happens when the pool is full.**
