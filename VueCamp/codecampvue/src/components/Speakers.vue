<template>
    <div>
       <CreateSpeaker/>
       <ViewSpeaker/>
    </div>
</template>
<script>
import CreateSpeaker from '@/components/CreateSpeaker';
import ViewSpeaker from '@/components/ViewSpeaker';

export default {
    components:{
        CreateSpeaker,
        ViewSpeaker  
    }
}
</script>