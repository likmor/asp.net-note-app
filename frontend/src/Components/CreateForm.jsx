import {
  Button,
  Input,
  Textarea,
  FormControl,
  FormLabel,
} from "@chakra-ui/react";
import { useState } from "react";

const CreateForm = ({ sendForm }) => {
  const [note, setNote] = useState({ title: "", description: "" });

  const onSubmit = (e) => {
    e.preventDefault();
    sendForm(note);
    setNote({ ...note, title: "", description: "" });
  };
  return (
    <form className="w-full flex flex-col gap-3" onSubmit={onSubmit}>
      <h3 className="font-bold text-2xl">Create Note</h3>
      <FormControl isRequired>
        <Input
          isRequired
          placeholder="Title"
          value={note?.title ?? ""}
          onChange={(e) => setNote({ ...note, title: e.target.value })}
        ></Input>
      </FormControl>

      <Textarea
        placeholder="Description"
        value={note?.description ?? ""}
        onChange={(e) => setNote({ ...note, description: e.target.value })}
      ></Textarea>
      <Button colorScheme="blue" type="submit">
        Submit
      </Button>
    </form>
  );
};
export default CreateForm;
