const TodoRow = (props) => {
    return (
      <tr>
        {props.item && <td>{props.item.action}</td>} {/* Check if item exists */}
        <td>
          <input
            type="checkbox"
            checked={props.item?.done || false} // Use optional chaining
            onChange={() => props.callback(props.item)}
          />
        </td>
      </tr>
    );
  };

  export default TodoRow;