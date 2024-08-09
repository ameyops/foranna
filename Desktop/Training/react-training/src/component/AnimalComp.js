import React from 'react'
import Animal from './Animal'

const AnimalContainer = () => {
  let animals = [
    { name: "Tiger", count: 500, isEndangered: true, photo: 'https://picsum.photos/200/300' },
    { name: "Brown Bear", count: 1500, isEndangered: false, photo: 'https://picsum.photos/200/300' },
    { name: "Panda", count: 200, isEndangered: true, photo: 'https://picsum.photos/200/300' }
];

return (
    <>
        <Animal list={animals}>
            <h1>Endangered Animals</h1>
        </Animal>
    </>
);
}

export default AnimalContainer