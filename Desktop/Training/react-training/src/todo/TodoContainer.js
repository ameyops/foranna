import { useState } from "react";
import TodoBanner from "./Banner";
import TodoRow from "./TodoRow";
import TodoCreator from "./TodoCreater";

export default function TodoContainer(){
    let name = "Alice"
    const todoList = [
        {action: "Attend Meeting", done: true},
        {action: "Buy Flowers", done: true},
        {action: "Book Train Tickets", done: false},
        {action: "Go to Gym", done: true},
    ];
    const [todoItems, setTodoItems] = new useState(todoList)
    // add the new todo item received form the child to the todoItems state
    // variable
    const addNewTodoItem = (newItem) => {
        if (!todoItems.find(x => x.action === newItem)) {
            setTodoItems([...todoItems, {action: newItem, done: false}])
        }
    }
    
    // toggle the done status every time the checked event is trigger from the tr element
    const toggleTodo = (item) => setTodoItems(
        todoItems.map(x => x.action === item.action ? {...x, done : !x.done} : x)
    )

    // call the array.map method to generate TodoRow for every element in the todoitems
    const generateTodoRow = todoItems.map((x, i) => <TodoRow key={i} 
        item={x} callback={toggleTodo} />)
    return <>
        <TodoBanner name={name} task={todoItems} />

        <table className="table table-bordered table-striped">
            <thead>
                <tr>
                    <th>Description</th>
                    <th>Done</th>
                </tr>
            </thead>
            <tbody>
                {generateTodoRow}
            </tbody>
        </table>
        <TodoCreator callback={addNewTodoItem} />
    </>
}