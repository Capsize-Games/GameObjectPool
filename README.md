# GameObjectPool

Easily create and access pools of GameObjects to save on memory.

`GameObjectPool.Pool` extends `System.Collections.Generic.Queue` and takes
instructions in the form of `PoolSettings` objects.

## PoolSettings

- `Name` - the name of the pool
- `Prefab` - the prefab item that will fill the pool
- `Starting Item Count` - the number of items to initially create
- `Max Item Count` - the number of items to add to the pool before triggering an error message
- `Parent` - transform to parent pooled objects to
- `Allow Unrestricted Growth` - allow pool to continue populating itself with objects after max reached

## Usage

**Install:** See *INSTALLATION.md* on how to import into a project.

1. Assign `GameObjectPool.PoolManager` to a game object in your main scene.
2. On that script set Pool to a number greater than zero -- these are the number
of pools you are creating.
3. A list of properties under the dropdown "Element N" will show up, fill out those properties
4. At run time the queue is created and `GameObjectPool.PooledItem` is attached to each instantiated prefab (which was assigned in step 3)
5. To get an object from the pool `GameObjectPool.PoolManager.Get("PoolName")`
6. To return an object to the pool, simply deactivate from whichever script is using it with `pooledObject.SetActive(false)` or see PooledItem.cs for self deactivating example - the item will be added back to the queue.

## Examples

See **Example > Scenes > Test** for sample usage involving shape spawning.
