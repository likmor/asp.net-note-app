import {
  Input,
  InputGroup,
  InputRightAddon,
  InputRightElement,
} from "@chakra-ui/react";
import { useEffect, useState } from "react";
import { SearchIcon } from "@chakra-ui/icons";

const CreateFilter = ({ notes, setNotes }) => {
  const [query, setQuery] = useState("");
  useEffect(() => {
    let filteredNotes = notes?.filter((note) =>
      note.title.toLowerCase().includes(query.toLowerCase())
    );
    setNotes(filteredNotes);
  }, [query, notes]);
  return (
    <InputGroup>
      <Input value={query} onChange={(e) => setQuery(e.target.value)}></Input>
      <InputRightElement>
        <SearchIcon />
      </InputRightElement>
    </InputGroup>
  );
};
export default CreateFilter;
