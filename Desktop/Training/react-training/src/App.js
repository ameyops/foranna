
import 'bootstrap'
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
// import { Counter } from './component/Counter';
// import ImpComp from './component/ImpComp';
// import Welcome from './component/Welcome';
// import AnimalContainer from './component/AnimalComp';
// import TodoBanner from './todo/Banner';
import TodoContainer from './todo/TodoContainer';

function App() {
  return (
    <div className="App">
            <TodoContainer />
    </div>
  );
}

export default App;
