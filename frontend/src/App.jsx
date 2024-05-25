import { Button, Input, Textarea } from "@chakra-ui/react";
import MyCard from "./Components/MyCard";
import CreateForm from "./Components/CreateForm";
import { useEffect, useState } from "react";
import {
  createNote,
  fetchNotes,
  deleteNote,
  updateNote,
} from "./services/Notes";
import CreateFilter from "./Components/CreateFilter";

const App = () => {
  const [notes, setNotes] = useState([]);
  const [filteredNotes, setFilteredNotes] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      let data = await fetchNotes();
      setNotes(data);
    };
    fetchData();
  }, []);

  const onSubmit = async (note) => {
    await createNote(note);
    let data = await fetchNotes();
    setNotes(data);
  };

  const onDeleteNote = async (id) => {
    await deleteNote(id);
    let data = await fetchNotes();
    setNotes(data);
  };

  const onUpdateNote = async (note) => {
    await updateNote(note);
    let data = await fetchNotes();
    setNotes(data);
  };

  return (
    <section className="p-8 flex flex-row gap-3">
      <div className="p-2 flex flex-col w-2/5 gap-8">
        <CreateForm sendForm={onSubmit} />
        <CreateFilter notes={notes} setNotes={setFilteredNotes}></CreateFilter>
      </div>
      <div className="w-full">
        <ul className="flex flex-col gap-3">
          {filteredNotes.length == 0 ? (
            <h3 className="font-bold text-2xl underline">No notes added</h3>
          ) : (
            filteredNotes.map((note) => (
              <li key={note.id}>
                <MyCard
                  id={note.id}
                  title={note.title}
                  description={note.description}
                  time={note.time}
                  deleteNote={onDeleteNote}
                  updateNote={onUpdateNote}
                ></MyCard>
              </li>
            ))
          )}
        </ul>
      </div>
    </section>
  );
};

export default App;
