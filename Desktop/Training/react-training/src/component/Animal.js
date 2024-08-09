import React from 'react'

const Image = (props) => {
  return <img src={props.path} alt={props.title} />;
}
const AnimalDetails = (props) => {
  let { name, count, isEndangered } = props.item;

  return (
      <li>
          <Image path={props.item.photo} title={name} />
          <p>Name: {name}</p>
          <p>Count: {count}</p>
          <p>Endangered: {isEndangered ? "Yes" : "No"}</p>
      </li>
  );
}
const Animal = (props) => {
  let { children, list } = props;

  const getList = list.map((element, index) => (
      <AnimalDetails key={index} item={element} />
  ));

  return (
      <>
          {children}
          <ul>
              {getList}
          </ul>
      </>
  );
}

export default Animal