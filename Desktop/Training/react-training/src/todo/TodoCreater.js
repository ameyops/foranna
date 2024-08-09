import { useState } from "react";
const TodoCreator =(props) => {
    const [newItem,setNewItem] = useState('');
    const createNewTodo =() =>{
        props.callback(newItem)
        setNewItem('')

    }
    return <>
    <input value= {newItem} onChange={(e) =>setNewItem(e.target.value)}/>
    <button className='btn btn-primary m-1 p-2'
    type="button"
    onClick={createNewTodo}
    >
        Add new </button>
    
    </>
}
export default TodoCreator;