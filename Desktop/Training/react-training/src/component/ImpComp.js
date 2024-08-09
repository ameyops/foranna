import React , {useState}from 'react'
import { DisplayComponent } from './DisplayComponent'


const ImpComp = () => {

const[userName,setUserName] = useState('mn')
const[role,setRole] = useState('')

  return (
   <>
   <input value={userName} onChange={(e) => setUserName(e.target.value)}/>
   <input value={role} onChange={(e) => setRole(e.target.value)}/>

   <DisplayComponent name={userName} role={role}/>
   </>
  )
};
export default  ImpComp;


