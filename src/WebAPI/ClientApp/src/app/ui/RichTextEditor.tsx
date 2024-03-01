// QuillEditor.tsx
import React from 'react';
import ReactQuill from 'react-quill';

interface RichTextEditorProps {
    onChange: (content: string, delta: any, source: string, editor: any) => void;
    value: string;
}

const RichTextEditor: React.FC<RichTextEditorProps> = ({ onChange, value, height }) => {
    return (
        <ReactQuill
            value={value}
            onChange={onChange}
            style={{ height: '85%', width: '100%' }}  // 设置编辑器的宽度和高度
        />
    );
};

export default RichTextEditor;
