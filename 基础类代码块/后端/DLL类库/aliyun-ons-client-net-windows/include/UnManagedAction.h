#ifndef __UNMANAGEDACTION_H__
#define __UNMANAGEDACTION_H__

//consuming result
enum UnmanagedAction{
//consume success, application could continue to consume next message
UnmanagedCommitMessage,
//consume fail, server will deliver this message later, application could continue to consume next message
UnmanagedReconsumeLater
};
#endif

