import React, {useState}from 'react'

export const Counter = () => {

    let [count , setcount] = useState(0);
    
    const increment = () => {
        setcount(count++);
        console.log(count)
        
    }
  return (
    <div>
        <h1>{count}</h1>
        <button onClick={increment}></button>
    </div>  
  )
}
